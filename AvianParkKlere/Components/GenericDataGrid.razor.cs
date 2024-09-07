using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AvianParkKlere.Components;

public partial class GenericDataGrid<TGet, TCreate, TUpdate>
    where TGet : class, new()
    where TCreate : class, new()
    where TUpdate : class, new()
{
    [CascadingParameter]
    protected MudTheme MyCustomTheme { get; set; }

    [Parameter] public bool EnableCrudOptions { get; set; }
    [Parameter] public bool RemoveSearchBar { get; set; } = false;
    [Parameter] public List<string> SearchFields { get; set; } = new();
    [Parameter] public EventCallback OnCreateNewItem { get; set; }
    [Parameter] public EventCallback<TGet> OnUpdateItem { get; set; }
    [Parameter] public EventCallback<TGet> OnDeleteItem { get; set; }
    [Parameter] public EventCallback<IEnumerable<TGet>> OnDeleteItems { get; set; }
    [Parameter] public EventCallback<IEnumerable<TGet>> OnDeleteSelectedItems { get; set; }
    [Parameter] public EventCallback OnExportAllToCSV { get; set; }
    [Parameter] public EventCallback OnExportAllToPDF { get; set; }
    [Parameter] public EventCallback<IEnumerable<TGet>> OnExportSelectedToCSV { get; set; }
    [Parameter] public EventCallback<IEnumerable<TGet>> OnExportSelectedToPDF { get; set; }
    [Parameter, Required] public List<string>? HeaderTitles { get; set; }
    [Parameter, Required] public IEnumerable<TGet>? Items { get; set; }
    [Parameter] public bool NoItemsFound { get; set; } = false;
    [Parameter] public string ItemsName { get; set; } = "Items";
    [Parameter] public EventCallback<IEnumerable<TGet>> SelectedItemsChanged { get; set; }
    [Parameter] public RenderFragment<TGet>? RowTemplate { get; set; }
    [Parameter] public EventCallback<string> OnSearchQueryChanged { get; set; }
    [Parameter] public EventCallback<string> OnSearchFieldChange { get; set; }
    [Parameter] public string SearchField { get; set; }

    public HashSet<TGet> SelectedItems = new();
    public MudTable<TGet> Table { get; set; }



    protected override async Task OnInitializedAsync()
    {
        SearchField = SearchFields.Count > 1 ? SearchFields[0] : "";
        if (OnSearchFieldChange.HasDelegate)
        {
            await OnSearchFieldChange.InvokeAsync(SearchField);
        }
    }


    private void OnSearch(string searchQuery)
    {

        if (OnSearchQueryChanged.HasDelegate)
        {
            OnSearchQueryChanged.InvokeAsync(searchQuery);
        }

        /*
        foreach (var field in SearchFields)
        {
            if (Regex.Replace(field, @"\s+", "").ToLower() == SearchField.ToLower())
            {
                Items = Items.Where(x => x.GetType().GetProperty(field)?.GetValue(x)?.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase) == true);
            } 
            StateHasChanged();
        }*/
    }


    private void OnSearchFieldSelect(string field)
    {
        if (OnSearchFieldChange.HasDelegate)
        {
            OnSearchFieldChange.InvokeAsync(field);
        }
    }


    private async Task OnSelectedItemsChanged()
    {
        if (SelectedItemsChanged.HasDelegate)
        {
            await SelectedItemsChanged.InvokeAsync(SelectedItems);
        }
    }


    private async Task OpenCreateDialogAsync()
    {
        if (OnCreateNewItem.HasDelegate)
        {
            await OnCreateNewItem.InvokeAsync();
        }
    }


    private async Task OpenUpdateDialogAsync(TGet item)
    {
        if (OnUpdateItem.HasDelegate)
        {
            await OnUpdateItem.InvokeAsync(item);
        }
    }


    private async Task OpenDeleteDialogAsync(TGet item)
    {
        if (OnDeleteItem.HasDelegate)
        {
            await OnDeleteItem.InvokeAsync(item);
        }
    }


    private async Task DeleteAllItemsAsync()
    {
        if (OnDeleteItems.HasDelegate && Items != null)
        {
            await OnDeleteItems.InvokeAsync(Items);
        }
    }


    private async Task DeleteSelectedItemsAsync()
    {
        if (OnDeleteItems.HasDelegate && Items != null)
        {
            await OnDeleteSelectedItems.InvokeAsync(Items);
        }
    }


    private async Task DeleteSelectedItemsAsync(IEnumerable<TGet> selectedItems)
    {
        if (OnDeleteItems.HasDelegate)
        {
            await OnDeleteItems.InvokeAsync(selectedItems);
        }
    }

    private async Task ExportAllToCSVAsync()
    {
        if (OnExportAllToCSV.HasDelegate)
        {
            await OnExportAllToCSV.InvokeAsync();
        }
    }

    private async Task ExportAllToPDFAsync()
    {
        if (OnExportAllToPDF.HasDelegate)
        {
            await OnExportAllToPDF.InvokeAsync();
        }
    }

    private async Task ExportSelectedToCSVAsync(IEnumerable<TGet> selectedItems)
    {
        if (OnExportSelectedToCSV.HasDelegate)
        {
            await OnExportSelectedToCSV.InvokeAsync(selectedItems);
        }
    }

    private async Task ExportSelectedToPDFAsync(IEnumerable<TGet> selectedItems)
    {
        if (OnExportSelectedToPDF.HasDelegate)
        {
            await OnExportSelectedToPDF.InvokeAsync(selectedItems);
        }
    }

}