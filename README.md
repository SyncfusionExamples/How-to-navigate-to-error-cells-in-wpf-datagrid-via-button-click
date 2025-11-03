# How to Navigate to Error Cells in WPF / UWP DataGrid via Button Click?

This example explains about how to navigate to error cells in [WPF DataGrid](https://www.syncfusion.com/wpf-ui-controls/datagrid) and [UWP DataGrid](https://www.syncfusion.com/uwp-ui-controls/datagrid) (SfDataGrid) via button click.

DataGrid does not have support to navigate through the error cells specifically. You can navigate to the error cells using the [CurrentRowColumnIndex](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.GridCurrentCellManager.html#Syncfusion_UI_Xaml_Grid_GridCurrentCellManager_CurrentRowColumnIndex) and [GridCell.HasError](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.GridCell.html#Syncfusion_UI_Xaml_Grid_GridCell_HasError) properties through button click.

``` csharp
private void OnFindNextClicked(object obj)
{
    var datagrid = obj as SfDataGrid;
    var visualContainer = datagrid.GetVisualContainer();
    var moveNextRow = true;
    var currentRowColumnIndex = datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
    //if there is no selection maintained in grid, currentRowColumnIndex will be -1. In that case we need to navigate to the first error cell.
    if (currentRowColumnIndex.RowIndex < 0)
        MoveToErrorCell(datagrid, true, ref moveNextRow);

    var rowIndex = currentRowColumnIndex.RowIndex;
    var columnIndex = currentRowColumnIndex.ColumnIndex;
    while (moveNextRow && rowIndex >= datagrid.GetFirstDataRowIndex() && rowIndex <= datagrid.GetLastDataRowIndex())
    {
        var gridCellCollections = GetGridCellCollection(datagrid, new RowColumnIndex(rowIndex,columnIndex));

        if (gridCellCollections != null)
        {
            foreach (var gridCellItem in gridCellCollections)
            {
                var gridCell = gridCellItem as GridCell;
                if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex > columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
                {
                    datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, gridCell.ColumnBase.ColumnIndex));
                    datagrid.ScrollInView(datagrid.SelectionController. CurrentCellManager.CurrentRowColumnIndex);                         
                    moveNextRow = false;
                    return;
                }
            }
        }

        rowIndex++;
        if (rowIndex <= datagrid.GetLastDataRowIndex())
            datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, datagrid.GetFirstColumnIndex()));
    }
}
 
/// <summary>
/// To get the grid cell collection of particular row
/// </summary>
private UIElementCollection GetGridCellCollection(SfDataGrid datagrid, RowColumnIndex currentRowColumnIndex)
{
    var currentRow = datagrid.RowGenerator.Items.FirstOrDefault(row => row.RowIndex == currentRowColumnIndex.RowIndex);
    var rowControl = currentRow.Element as VirtualizingCellsControl;
    var orientedCellsPanel = rowControl.Content as OrientedCellsPanel;
    return orientedCellsPanel.Children;
}
```