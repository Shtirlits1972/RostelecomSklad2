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
    public partial class DepartmentsPage : Page
    {
        List<Departments> DepartmentsList = new List<Departments>();
        public DepartmentsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DepartmentsEdit edit = new DepartmentsEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Departments model = edit.model;

            if (model != null && model.Id > 0)
            {
                DepartmentsList.Add(model);

                mainGrid.ItemsSource = DepartmentsList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                DepartmentsEdit edit = new DepartmentsEdit();
                edit.IsEdit = true;
                Departments model = (Departments)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < DepartmentsList.Count; i++)
                {
                    Departments item = DepartmentsList[i];

                    if (model.Id == item.Id)
                    {
                        DepartmentsList[i] = model;
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
                    Departments model = (Departments)mainGrid.SelectedItem;
                    DepartmentsCrud.Del(model.Id);

                    DepartmentsList.Remove(model);

                    mainGrid.ItemsSource = DepartmentsList;
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
            DepartmentsList = DepartmentsCrud.GetAll();
            mainGrid.ItemsSource = DepartmentsList;
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
                Departments model = (Departments)e.EditedItem;

                DepartmentsCrud.Edit(model);

                for (int i = 0; i < DepartmentsList.Count; i++)
                {
                    if (DepartmentsList[i].Id == model.Id)
                    {
                        DepartmentsList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = DepartmentsList;
                mainGrid.Rebind();
            }
        }
    }
}
