using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.ServerUser.Components.CrudDialogs.Create
{
    public partial class CreateClothingDialog
    {
        [Parameter] public EventCallback<ClothingPostDto> OnSubmit { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        ClothingPostDto Clothing = new();
        private bool Validated = false;

        private async Task Submit()
        {
            await OnSubmit.InvokeAsync(Clothing);
        }

        private void ValidateForm()
        {
            if (Clothing.Name is not null && Clothing.Name.Length > 1
                && Clothing.SizeMeasurement is not null && Clothing.SizeMeasurement.Length > 1)
            {
                Validated = true;
            }
            else
            {
                Validated = false;
            }
        }

    }
}
