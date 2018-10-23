using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SfDataGridDemo
{
    class ViewModel : INotifyPropertyChanged
    {
        private ICommand findNextCommand;
        public ICommand FindNextCommand
        {
            get { return findNextCommand; }
            set
            {
                findNextCommand = value;
                OnPropertyChanged("FindNextCommand");
            }
        }

        private ICommand findPreviousCommand;
        public ICommand FindPreviousCommand
        {
            get { return findPreviousCommand; }
            set
            {
                findPreviousCommand = value;
                OnPropertyChanged("FindPreviousCommand");
            }
        }

        private DataTable employeesTable;
        public DataTable EmployeesTable
        {
            get { return employeesTable; }
            set
            {
                employeesTable = value;
                OnPropertyChanged("EmployeesTable");
            }
        }

        #region Constructor

        public ViewModel()
        {
            PopulateDataTable();
            findNextCommand = new CustomCommand(OnFindNextClicked);
            findPreviousCommand = new CustomCommand(OnFindPreviousClicked);
        }

        private void PopulateDataTable()
        {
            this.employeesTable = new DataTable();
            var column1 = new System.Data.DataColumn("EmployeeName");
            var column2 = new System.Data.DataColumn("EmployeeAge");
            var column3 = new System.Data.DataColumn("EmployeeArea");
            var column4 = new System.Data.DataColumn("EmployeeSalary");
            var column5 = new System.Data.DataColumn("ExperienceInMonth");
            var column6 = new System.Data.DataColumn("EmployeeGender");
            var column7 = new System.Data.DataColumn("Rating");
            this.employeesTable.Columns.Add(column1);
            this.employeesTable.Columns.Add(column2);
            this.employeesTable.Columns.Add(column3);
            this.employeesTable.Columns.Add(column4);
            this.employeesTable.Columns.Add(column5);
            this.employeesTable.Columns.Add(column6);
            this.employeesTable.Columns.Add(column7);

            var row1 = this.employeesTable.Rows.Add("Mart", 45, "USA", 33000, 10, "Male", 2);
            row1.SetColumnError(column1, "error");
            row1.SetColumnError(column3, "error");
            row1.SetColumnError(column7, "error");
            var row2 = this.employeesTable.Rows.Add("Peter", 35, "UK", 5678, 10, "Male", 1);
            row2.SetColumnError(column1, "error");
            row2.SetColumnError(column3, "error");
            var row3 = this.employeesTable.Rows.Add("James", 42, "UAE", 18700, 10, "Male", 3);
            var row4 = this.employeesTable.Rows.Add("Oliver", 36, "USA", 67000, 10, "Male", 4);
            var row5 = this.employeesTable.Rows.Add("Robert", 54, "India", 34567, 10, "Male", 5);
            row5.SetColumnError(column6, "error");
            var row6 = this.employeesTable.Rows.Add("Suji", 45, "UK", 90000, 10, "Female", 2);
            var row7 = this.employeesTable.Rows.Add("Mahesh", 48, "UK", 34567, 10, "Male", 1);
            var row8 = this.employeesTable.Rows.Add("Ruby", 49, "UK", 12345, 10, "Female", 3);
            row8.SetColumnError(column4, "error");
            row8.SetColumnError(column7, "error");
            var row9 = this.employeesTable.Rows.Add("Christain", 54, "India", 80000, 10, "Male", 4);
            var row10 = this.employeesTable.Rows.Add("Aravind", 65, "India", 12000, 10, "Male", 5);
            var row11 = this.employeesTable.Rows.Add("Daniel", 56, "USA", 16000, 10, "Male", 2);
            var row12 = this.employeesTable.Rows.Add("Suhitha Azar", 78, "UK", 98789, 10, "Female", 3);
            var row13 = this.employeesTable.Rows.Add("Praveen", 54, "UAE", 45678, 10, "Male", 3);
            row13.SetColumnError(column2, "error");
            row13.SetColumnError(column3, "error");
            var row14 = this.employeesTable.Rows.Add("Stephen", 45, "USA", 21000, 10, "Male", 1);
            var row15 = this.employeesTable.Rows.Add("Asha Joseph", 56, "India", 56787, 10, "Female", 4);
            var row16 = this.employeesTable.Rows.Add("Clarke", 67, "UK", 1200, 10, "Male", 2);
            row16.SetColumnError(column5, "error");
            var row17 = this.employeesTable.Rows.Add("Dhileep Venkatesh", 45, "UK", 45656, 10, "Male", 5);
            row17.SetColumnError(column1, "error");
            row17.SetColumnError(column7, "error");
        }

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
                var gridCellCollections = GetGridCellCollection(datagrid, new RowColumnIndex(rowIndex, columnIndex));

                if (gridCellCollections != null)
                {
                    foreach (var gridCellItem in gridCellCollections)
                    {
                        var gridCell = gridCellItem as GridCell;
                        if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex > columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
                        {
                            datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, gridCell.ColumnBase.ColumnIndex));
                            datagrid.ScrollInView(datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex);
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

        private void OnFindPreviousClicked(object obj)
        {
            var datagrid = obj as SfDataGrid;
            var visualContainer = datagrid.GetVisualContainer();
            var movePreviousRow = true;

            var currentRowColumnIndex = datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            //if there is no selection maintained in grid, currentRowColumnIndex will be -1. In that case we need to navigate to the last error cell.
            if (currentRowColumnIndex.RowIndex < 0)
                MoveToErrorCell(datagrid, false, ref movePreviousRow);

            var rowIndex = currentRowColumnIndex.RowIndex;
            var columnIndex = currentRowColumnIndex.ColumnIndex;
            while (movePreviousRow && rowIndex >= datagrid.GetFirstDataRowIndex() && rowIndex <= datagrid.GetLastDataRowIndex())
            {
                var gridCellCollections = GetGridCellCollection(datagrid, new RowColumnIndex(rowIndex, columnIndex));
                if (gridCellCollections != null)
                {
                    for (int i = gridCellCollections.Count - 1; i >= 0; i--)
                    {
                        var gridCell = gridCellCollections[i] as GridCell;
                        if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex < columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
                        {
                            datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex, gridCell.ColumnBase.ColumnIndex));
                            datagrid.ScrollInView(datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex);
                            movePreviousRow = false;
                            return;
                        }
                    }
                }

                rowIndex--;
                if (rowIndex >= datagrid.GetFirstDataRowIndex())
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

        /// <summary>
        /// To find the first/last error cell in view if selection is not maintained in datagrid.
        /// </summary>
        private void MoveToErrorCell(SfDataGrid datagrid, bool MoveToFirstCell, ref bool moveToRow)
        {
            RowColumnIndex rowColumnIndex;
            if (MoveToFirstCell)
                rowColumnIndex = new RowColumnIndex(datagrid.GetFirstDataRowIndex(), datagrid.GetFirstColumnIndex());
            else
                rowColumnIndex = new RowColumnIndex(datagrid.GetLastDataRowIndex(), datagrid.GetLastColumnIndex());

            datagrid.SelectionController.MoveCurrentCell(rowColumnIndex);
            var currentRowColumnIndex = datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex;
            var gridCellCollections = GetGridCellCollection(datagrid, currentRowColumnIndex);

            if (gridCellCollections != null)
            {
                foreach (var gridCellItem in gridCellCollections)
                {
                    var gridCell = gridCellItem as GridCell;
                    if (gridCell != null && gridCell.HasError && gridCell.ColumnBase.ColumnIndex == currentRowColumnIndex.ColumnIndex)
                    {
                        datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(currentRowColumnIndex.RowIndex, gridCell.ColumnBase.ColumnIndex));
                        datagrid.ScrollInView(datagrid.SelectionController.CurrentCellManager.CurrentRowColumnIndex); ;
                        moveToRow = false;
                        return;
                    }
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        

    }
}
