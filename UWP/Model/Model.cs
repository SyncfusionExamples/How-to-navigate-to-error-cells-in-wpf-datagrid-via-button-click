//using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfDataGridDemo
{
    class BusinessObjects : INotifyPropertyChanged
    {
        private int rating;
        [Range(3, 5, ErrorMessage = "Rating should not be less than 3")]
        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }

        private string _ename;
        [StringLength(7,ErrorMessage ="Name length should not be greater than 5")]
        public string EmployeeName
        {
            get
            {
                return _ename;
            }
            set
            {
                _ename = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        private int _eage;
        [Range(20, 50, ErrorMessage = "Employee Age should be between 20 and 50")]
        public int EmployeeAge
        {
            get
            {
                return _eage;
            }
            set
            {
                _eage = value;
                OnPropertyChanged("EmployeeAge");
            }
        }

        private string _earea;
        public string EmployeeArea
        {
            get
            {
                return _earea;
            }
            set
            {
                _earea = value;
                OnPropertyChanged("EmployeeArea");
            }
        }

        private string _egender;
        public string EmployeeGender
        {
            get
            {
                return _egender;
            }
            set
            {
                _egender = value;
                OnPropertyChanged("EmployeeGender");
            }
        }

        private int _eExpInMonth;
        public int ExperienceInMonth
        {
            get
            {
                return _eExpInMonth;
            }
            set
            {
                _eExpInMonth = value;
                OnPropertyChanged("ExperienceInMonth");
            }
        }

        private double _esalary;
        [Range(0, 50000, ErrorMessage = "EmployeeSalary should not be greater than 30000")]
        public double EmployeeSalary
        {
            get
            {
                return _esalary;
            }
            set
            {
                _esalary = value;
                OnPropertyChanged("EmployeeSalary");
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
}
