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
    public partial class SupplayPage : Page
    {
        List<Supplay> SupplayList = new List<Supplay>();
        public SupplayPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SupplayEdit edit = new SupplayEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Supplay model = edit.model;

            if (model != null && model.Id > 0)
            {
                SupplayList.Add(model);

                mainGrid.ItemsSource = SupplayList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                SupplayEdit edit = new SupplayEdit();
                edit.IsEdit = true;
                Supplay model = (Supplay)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < SupplayList.Count; i++)
                {
                    Supplay item = SupplayList[i];

                    if (model.Id == item.Id)
                    {
                        SupplayList[i] = model;
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
                    Supplay model = (Supplay)mainGrid.SelectedItem;
                    SupplayCrud.Del(model.Id);

                    SupplayList.Remove(model);

                    mainGrid.ItemsSource = SupplayList;
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
            SupplayList = SupplayCrud.GetAll();
            mainGrid.ItemsSource = SupplayList;
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
                Supplay model = (Supplay)e.EditedItem;

                SupplayCrud.Edit(model);

                for (int i = 0; i < SupplayList.Count; i++)
                {
                    if (SupplayList[i].Id == model.Id)
                    {
                        SupplayList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = SupplayList;
                mainGrid.Rebind();
            }
        }
    }
}
