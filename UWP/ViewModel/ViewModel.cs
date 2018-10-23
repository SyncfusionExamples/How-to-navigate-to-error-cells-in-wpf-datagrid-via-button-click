//using Syncfusion.Windows.Shared;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SfDataGridDemo
{
    class ViewModel : INotifyPropertyChanged
    {
        EmployeeDetails emp = new EmployeeDetails();

        #region Constructor

        public ViewModel()
        {
            this.Employees = emp;
            findNextCommand = new CustomCommand(OnFindNextClicked);
            findPreviousCommand = new CustomCommand(OnFindPreviousClicked);
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
                var gridCellCollections = GetGridCellCollection(datagrid, new RowColumnIndex(rowIndex,columnIndex));

                if (gridCellCollections != null)
                {
                    foreach (var gridCellItem in gridCellCollections)
                    {
                        var gridCell = gridCellItem as GridCell;
                        if (gridCell != null && gridCell.HasError && (gridCell.ColumnBase.ColumnIndex > columnIndex || currentRowColumnIndex.RowIndex != rowIndex))
                        {
                            datagrid.SelectionController.MoveCurrentCell(new RowColumnIndex(rowIndex,gridCell.ColumnBase.ColumnIndex));
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
                var gridCellCollections = GetGridCellCollection(datagrid, new RowColumnIndex(rowIndex,columnIndex));
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

        private ObservableCollection<BusinessObjects> employees;
        public ObservableCollection<BusinessObjects> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    #region GDCSource DataSource
    class EmployeeDetails : ObservableCollection<BusinessObjects>
    {
        Random rand = new Random();
        public EmployeeDetails()
        {
            PopulateCollection();
        }

        private void PopulateCollection()
        {
            this.Clear();
            BusinessObjects b = new BusinessObjects() { EmployeeName = "Mart", EmployeeAge = 45, EmployeeArea = "USA", EmployeeSalary = 33000, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 2 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Peter", EmployeeAge = 35, EmployeeArea = "UK", EmployeeSalary = 5678, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 1 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "James", EmployeeAge = 42, EmployeeArea = "UAE", EmployeeSalary = 18700, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 3 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Oliver", EmployeeAge = 36, EmployeeArea = "USA", EmployeeSalary = 67000, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 4 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Robert", EmployeeAge = 54, EmployeeArea = "India", EmployeeSalary = 34567, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 5 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Suji", EmployeeAge = 45, EmployeeArea = "UK", EmployeeSalary = 90000, ExperienceInMonth = 10, EmployeeGender = "Female", Rating = 2 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Mahesh", EmployeeAge = 48, EmployeeArea = "UK", EmployeeSalary = 34567, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 1 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Ruby", EmployeeAge = 49, EmployeeArea = "UK", EmployeeSalary = 12345, ExperienceInMonth = 10, EmployeeGender = "Female", Rating = 3 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Christain", EmployeeAge = 54, EmployeeArea = "India", EmployeeSalary = 80000, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 4 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Aravind", EmployeeAge = 65, EmployeeArea = "India", EmployeeSalary = 12000, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 5 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Daniel", EmployeeAge = 56, EmployeeArea = "USA", EmployeeSalary = 16000, ExperienceInMonth = 10, EmployeeGender = "Male", Rating = 2 };
            this.Add(b);
            b = new BusinessObjects() { EmployeeName = "Suhitha Azar", EmployeeAge = 78, EmployeeArea = "UK", EmployeeSalary = 98789, ExperienceInMonth = 10, EmployeeGender = "Female", Rating = 3 };
            this.Add(b);
        }
    }

    #endregion
}
