using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SfDataGridDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            //var visualContainer = this.datagrid.GetVisualContainer();
            //var movePreviousRow = true;

            //var currentRowColumnIndex = this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            //if (currentRowColumnIndex.RowIndex < 0)
            //{
            //    this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(this.datagrid.GetLastDataRowIndex(), this.datagrid.GetLastColumnIndex()));
            //    currentRowColumnIndex = this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            //}

            //var rowIndex = currentRowColumnIndex.RowIndex;
            //var columnIndex = currentRowColumnIndex.ColumnIndex;
            //while (movePreviousRow && rowIndex >= this.datagrid.GetFirstDataRowIndex() && rowIndex <= this.datagrid.GetLastDataRowIndex())
            //{
            //    var currentRow = this.datagrid.RowGenerator.Items.FirstOrDefault(row => row.RowIndex == rowIndex);
            //    if (currentRow != null)
            //    {
            //        var rowControl = currentRow.Element as VirtualizingCellsControl;
            //        var orientedCellsPanel = rowControl.Content as OrientedCellsPanel;
            //        var gridCellCollections = orientedCellsPanel.Children;
            //        for (int i = gridCellCollections.Count - 1; i >= 0; i--)
            //        {
            //            var gridCell = gridCellCollections[i] as GridCell;
            //            if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex < columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
            //            {
            //                this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, gridCell.ColumnBase.ColumnIndex));
            //                this.datagrid.ScrollInView(this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex);
            //                movePreviousRow = false;
            //                return;
            //            }
            //        }
            //    }

            //    rowIndex--;
            //    if (rowIndex >= this.datagrid.GetFirstDataRowIndex())
            //        this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, this.datagrid.GetFirstColumnIndex()));
            //}
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //var visualContainer = this.datagrid.GetVisualContainer();
            //var moveNextRow = true;

            //var currentRowColumnIndex = this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            //if(currentRowColumnIndex.RowIndex < 0)
            //{
            //    this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(this.datagrid.GetFirstDataRowIndex(), this.datagrid.GetFirstColumnIndex()));
            //    currentRowColumnIndex = this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            //}
            //var rowIndex = currentRowColumnIndex.RowIndex;
            //var columnIndex = currentRowColumnIndex.ColumnIndex;
            //while (moveNextRow && rowIndex >= this.datagrid.GetFirstDataRowIndex() && rowIndex <= this.datagrid.GetLastDataRowIndex())
            //{
            //    var currentRow = this.datagrid.RowGenerator.Items.FirstOrDefault(row => row.RowIndex == rowIndex);

            //    if (currentRow != null)
            //    {
            //        var rowControl = currentRow.Element as VirtualizingCellsControl;
            //        var orientedCellsPanel = rowControl.Content as OrientedCellsPanel;
            //        var gridCellCollections = orientedCellsPanel.Children;
                    
            //        foreach (var gridCellItem in gridCellCollections)
            //        {
            //            var gridCell = gridCellItem as GridCell;
            //            if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex > columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
            //            {
            //                this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, gridCell.ColumnBase.ColumnIndex));
            //                this.datagrid.ScrollInView(this.datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex);
            //                moveNextRow = false;
            //                return;
            //            }
            //        }
            //    }

            //    rowIndex++;
            //    if (rowIndex <= this.datagrid.GetLastDataRowIndex())
            //        this.datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, this.datagrid.GetFirstColumnIndex()));
            //}
        }
    }
}
