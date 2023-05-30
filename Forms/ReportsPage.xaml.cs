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
    public partial class ReportsPage : Page
    {
        List<Reports> ReportsList = new List<Reports>();
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ReportsEdit edit = new ReportsEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Reports model = edit.model;

            if (model != null && model.Id > 0)
            {
                ReportsList.Add(model);

                mainGrid.ItemsSource = ReportsList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                ReportsEdit edit = new ReportsEdit();
                edit.IsEdit = true;
                Reports model = (Reports)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < ReportsList.Count; i++)
                {
                    Reports item = ReportsList[i];

                    if (model.Id == item.Id)
                    {
                        ReportsList[i] = model;
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
                    Reports model = (Reports)mainGrid.SelectedItem;
                    ReportsCrud.Del(model.Id);

                    ReportsList.Remove(model);

                    mainGrid.ItemsSource = ReportsList;
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
            ReportsList = ReportsCrud.GetAll();
            mainGrid.ItemsSource = ReportsList;
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
                Reports model = (Reports)e.EditedItem;

                ReportsCrud.Edit(model);

                for (int i = 0; i < ReportsList.Count; i++)
                {
                    if (ReportsList[i].Id == model.Id)
                    {
                        ReportsList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = ReportsList;
                mainGrid.Rebind();
            }
        }
    }
}
