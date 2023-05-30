using RostelecomSklad2.CRUD;
using RostelecomSklad2.Models;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RostelecomSklad2.Forms
{
    public partial class EmployeeEdit : Window
    {
        public bool IsEdit = false;
        public Employee model = new Employee();
        public EmployeeEdit()
        {
            InitializeComponent();
            comboDepartmentsId.ItemsSource = DepartmentsCrud.GetAll();
            comboDepartmentsId.SelectedIndex = 0;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.EmployeeName = txtEmployeeName.Text;
            model.PersonalData = txtPersonalData.Text;
            model.Position = txtPosition.Text;

            Departments departments = (Departments)comboDepartmentsId.SelectedItem;
            model.DepartmentsId = departments.Id;
            model.DepartmentsName = departments.DepartmentsName;

            if (IsEdit)
            {
                EmployeeCrud.Edit(model);
            }
            else
            {
                model = EmployeeCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtEmployeeName.Text = model.EmployeeName;
                txtPersonalData.Text =  model.PersonalData;
                txtPosition.Text = model.Position;

                #region Departments
                for (int i = 0; i < comboDepartmentsId.Items.Count; i++)
                {
                    Departments tmp = (Departments)comboDepartmentsId.Items[i];
                    if (tmp.Id == model.DepartmentsId)
                    {
                        comboDepartmentsId.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
