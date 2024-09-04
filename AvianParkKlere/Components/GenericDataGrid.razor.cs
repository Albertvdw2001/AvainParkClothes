﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AvianParkKlere.Components;

public partial class GenericDataGrid<TGet, TCreate, TUpdate>
    where TGet : class, new()
    where TCreate : class, new()
    where TUpdate : class, new()
{
    [Parameter] public bool EnableCrudOptions { get; set; }
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

    public HashSet<TGet> SelectedItems = new();
    public MudTable<TGet> Table { get; set; }


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
