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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RostelecomSklad2.Forms
{
    public partial class EmployeePage : Page
    {
        List<Employee> EmployeeList = new List<Employee>();
        public EmployeePage()
        {
            InitializeComponent();
            colDepartmentsId.ItemsSource = DepartmentsCrud.GetAll();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeEdit edit = new EmployeeEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Employee model = edit.model;

            if (model != null && model.Id > 0)
            {
                EmployeeList.Add(model);

                mainGrid.ItemsSource = EmployeeList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                EmployeeEdit edit = new EmployeeEdit();
                edit.IsEdit = true;
                Employee model = (Employee)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < EmployeeList.Count; i++)
                {
                    Employee item = EmployeeList[i];

                    if (model.Id == item.Id)
                    {
                        EmployeeList[i] = model;
                        break;
                    }
                }
                mainGrid.Rebind();

            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Employee model = (Employee)mainGrid.SelectedItem;
                    EmployeeCrud.Del(model.Id);

                    EmployeeList.Remove(model);

                    mainGrid.ItemsSource = EmployeeList;
                    mainGrid.Rebind();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
        private void btnSel_Click(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void GetAll()
        {
            EmployeeList = EmployeeCrud.GetAll();
            mainGrid.ItemsSource = EmployeeList;
            mainGrid.Rebind();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in mainGrid.Columns)
            {
                column.ClearFilters();
            }
            mainGrid.FilterDescriptors.ResumeNotifications();
        }
        private void mainGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {
                Employee model = (Employee)e.EditedItem;

                EmployeeCrud.Edit(model);

                for (int i = 0; i < EmployeeList.Count; i++)
                {
                    if (EmployeeList[i].Id == model.Id)
                    {
                        EmployeeList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = EmployeeList;
                mainGrid.Rebind();
            }
        }
    }
}
