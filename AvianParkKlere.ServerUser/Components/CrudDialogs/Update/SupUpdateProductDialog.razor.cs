/*using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ScanAndGo.AdminPortal.Models.CompositeDtos;
using ScanAndGo.Contracts.Models.Dtos.Branch;
using ScanAndGo.Contracts.Models.Dtos.Product;
using ScanAndGo.Contracts.Models.Dtos.ProductDetails;
using System.ComponentModel.DataAnnotations;
using ScanAndGo.Contracts.Models.Dtos.Tenant;
using ScanAndGo.AdminPortal.Pages.SuperAdminPages;
using ScanAndGo.Contracts.Models.Entities;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Update
{
    public partial class SupUpdateProductDialog
    {
        [Parameter, Required] public UpdateProductDto Product { get; set; }
        [Parameter, Required] public List<GetTenantDto> Tenants { get; set; }
        [Parameter, Required] public List<GetBranchDto> Branches { get; set; }
        [Parameter, Required] public List<UpdateProductDetailsDto> ExistingProductDetails { get; set; } = new();
        [Parameter] public EventCallback<UpdateProductDto> OnUpdate { get; set; }

        private UpdateProductDto ProductCopy;
        private MudFileUpload<IBrowserFile> ImageEdit;
        private string? DialogImageType;
        private bool ProductChanged = false;
        private List<BranchProductDetailsComposite> BranchList = new();
        private List<BranchProductDetailsComposite> SelectedBranches = new(); // handles branch-price relationship
        private List<BranchProductDetailsComposite> SelectedBranchesCopy = new();


        protected override async Task OnInitializedAsync()
        {
            ProductCopy = mapper.Map<UpdateProductDto>(Product);
            ProductCopy.Image = Product.Image;

            GenerateBranchList(Product.TenantId);

            SetProductChanged();
        }


        private async Task RemoveImage()
        {
            await ImageEdit.ClearAsync();
            Product.Image = null;
            SetProductChanged();
            StateHasChanged();
        }


        private async Task OnBranchSelectionChanged(IReadOnlyCollection<BranchProductDetailsComposite> branches)
        {
            SelectedBranches = branches.ToList();
            SetProductChanged();
        }


        private void OnBranchPriceChecked(int branchId)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch is not null)
            {
                BranchList.FirstOrDefault(b => b.BranchId == branchId).Price = branch.Price == null ? Product.Price : null;
            }
        }


        // TODO: refactor these onchange methods to a single method somehow
        private void OnProductNameChanged(string name)
        {
            Product.Name = name;
            SetProductChanged();
        }

        private void OnProductPluChanged(string plu)
        {
            Product.PluNumber = plu;
            SetProductChanged();
        }

        private void OnProductDescriptionChanged(string description)
        {
            Product.Description = description;
            SetProductChanged();
        }

        private Task OnProductPriceChanged(decimal price)
        {
            Product.Price = price;
            SetProductChanged();

            return Task.CompletedTask;
        }


        private void OnTenantSelectionChanged(int tenantId)
        {
            Product.TenantId = tenantId;
            GenerateBranchList(tenantId);
        }


        private void GenerateBranchList(int tenantId)
        {
            BranchList = new();
            var tenantBranches = Branches.Where(b => b.TenantId == tenantId).ToList();

            foreach (var branch in tenantBranches)
            {
                var productDetail = ExistingProductDetails.FirstOrDefault(pd => pd.BranchId == branch.Id);

                var compositeDto = new BranchProductDetailsComposite
                {
                    BranchId = branch.Id,
                    Name = branch.Name,
                    Price = productDetail == null ? null : productDetail.Price,
                    IsAllocated = productDetail is not null
                };

                BranchList.Add(compositeDto);
            }
        }


        private async Task UndoFieldEdit(string field)
        {
            SetProductChanged();
            if (ProductChanged == true)
            {
                switch (field)
                {
                    case nameof(GetProductDto.Name):
                        Product.Name = ProductCopy.Name;
                        break;

                    case nameof(GetProductDto.Description):
                        Product.Description = ProductCopy.Description;
                        break;

                    case nameof(GetProductDto.PluNumber):
                        Product.PluNumber = ProductCopy.PluNumber;
                        break;

                    case nameof(GetProductDto.Price):
                        Product.Price = ProductCopy.Price;
                        break;

                    case nameof(GetProductDto.Image):
                        Product.Image = ProductCopy.Image;
                        await ImageEdit.ClearAsync();
                        break;
                }
            }
            SetProductChanged();
            StateHasChanged();
        }


        private async Task OnImageFieldChange(IBrowserFile file)
        {
            if (file == null)
            {
                return;
            }

            long fileSize = file.Size;
            long maxSizeInBytes = 500 * 1024; // 500 KB
            if (fileSize > maxSizeInBytes)
            {
                ShowErrorSnackbar(ls.GetText("FileExceeds500KbError"));
                return;
            }

            using (var memoryStream = new MemoryStream())
            {
                await using (var stream = file.OpenReadStream())
                {
                    await stream.CopyToAsync(memoryStream);
                }
                byte[] fileBytes = memoryStream.ToArray();
                DialogImageType = GetImageType(fileBytes);
                Product.Image = fileBytes;
            }

            SetProductChanged();
            StateHasChanged();
        }


        public string GetImageType(byte[] imageData)
        {
            if (imageData == null || imageData.Length < 4)
            {
                throw new ArgumentException(ls.GetText("InvalidImageData."));
            }

            // Check the magic numbers for common image formats
            if (imageData.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A })) // PNG
            {
                return "image/png";
            }
            if (imageData.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF })) // JPEG
            {
                return "image/jpeg";
            }
            if (imageData.Take(2).SequenceEqual(new byte[] { 0x42, 0x4D })) // BMP
            {
                return "image/bmp";
            }

            throw new NotSupportedException(ls.GetText("UnsupportedImageFormat."));
        }


        private void SetProductChanged()
        {
            if (Product.Name != ProductCopy.Name ||
                Product.Description != ProductCopy.Description ||
                Product.Price != ProductCopy.Price ||
                Product.PluNumber != ProductCopy.PluNumber ||
                Product.Image != ProductCopy.Image ||
                SelectedBranches != SelectedBranchesCopy)
            {
                ProductChanged = true;
            }
            else
            {
                ProductChanged = false;
            }
        }


        private async Task Update()
        {
            if (OnUpdate.HasDelegate)
            {
                Product.AddedProductDetailsDtos = new();
                Product.RemovedProductDetailsDtos = new();

                // populate Product.AddedDetails    
                foreach (var branch in BranchList)
                {
                    if (branch.IsAllocated == true)
                    {
                        if (ExistingProductDetails.Any(epd => epd.BranchId == branch.BranchId))
                        {
                            var existingDetail = ExistingProductDetails.FirstOrDefault(epd => epd.BranchId == branch.BranchId);
                            if (existingDetail.Price != branch.Price)
                            {
                                UpdateProductDetailsDto updatedDetails = new();
                                updatedDetails.BranchId = branch.BranchId;
                                updatedDetails.Price = branch.Price;
                                Product.AddedProductDetailsDtos.Add(updatedDetails);
                            }
                        }
                        else
                        {
                            UpdateProductDetailsDto createDetails = new();
                            createDetails.BranchId = branch.BranchId;
                            createDetails.Price = branch.Price;
                            Product.AddedProductDetailsDtos.Add(createDetails);
                        }
                    }
                }

                // populate Product.RemovedDetails
                foreach (var existingDetail in ExistingProductDetails)
                {
                    if (!BranchList.Any(sb => sb.BranchId == existingDetail.BranchId && sb.IsAllocated == true))
                    {
                        Product.RemovedProductDetailsDtos.Add(existingDetail);
                    }
                }

                await OnUpdate.InvokeAsync(Product);
            }
        }


        *//* Snackbar messages *//*

        private void ShowErrorSnackbar(string message)
        {
            Snackbar.Clear();
            Snackbar.Add(message, Severity.Error, config =>
            {
                config.ShowCloseIcon = true;
            });
        }

        private void ShowSuccessSnackbar(string message)
        {
            Snackbar.Clear();
            Snackbar.Add(message, Severity.Success, config =>
            {
                config.ShowCloseIcon = true;
            });
        }

    }
}
*/