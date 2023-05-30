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
    public partial class WarehousePage : Page
    {
        List<Warehouse> WarehouseList = new List<Warehouse>();
        public WarehousePage()
        {
            InitializeComponent();
            colProduct.ItemsSource = ProductCrud.GetAll();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WarehouseEdit edit = new WarehouseEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Warehouse model = edit.model;

            if (model != null && model.Id > 0)
            {
                WarehouseList.Add(model);

                mainGrid.ItemsSource = WarehouseList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                WarehouseEdit edit = new WarehouseEdit();
                edit.IsEdit = true;
                Warehouse model = (Warehouse)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                if(model != null)
                {
                    for (int i = 0; i < WarehouseList.Count; i++)
                    {
                        Warehouse item = WarehouseList[i];

                        if (model.Id == item.Id)
                        {
                            WarehouseList[i] = model;
                            break;
                        }
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
                    Warehouse model = (Warehouse)mainGrid.SelectedItem;
                    WarehouseCrud.Del(model.Id);

                    WarehouseList.Remove(model);

                    mainGrid.ItemsSource = WarehouseList;
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
            WarehouseList = WarehouseCrud.GetAll();
            mainGrid.ItemsSource = WarehouseList;
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
                Warehouse model = (Warehouse)e.EditedItem;

                WarehouseCrud.Edit(model);

                for (int i = 0; i < WarehouseList.Count; i++)
                {
                    if (WarehouseList[i].Id == model.Id)
                    {
                        WarehouseList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = WarehouseList;
                mainGrid.Rebind();
            }
        }
    }
}
