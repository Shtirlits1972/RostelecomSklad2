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
    public partial class ProductPage : Page
    {
        List<Product> ProductList = new List<Product>();
        public ProductPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductEdit edit = new ProductEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Product model = edit.model;

            if (model != null && model.Id > 0)
            {
                ProductList.Add(model);

                mainGrid.ItemsSource = ProductList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                ProductEdit edit = new ProductEdit();
                edit.IsEdit = true;
                Product model = (Product)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < ProductList.Count; i++)
                {
                    Product item = ProductList[i];

                    if (model.Id == item.Id)
                    {
                        ProductList[i] = model;
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
                    Product model = (Product)mainGrid.SelectedItem;
                    ProductCrud.Del(model.Id);

                    ProductList.Remove(model);

                    mainGrid.ItemsSource = ProductList;
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
            ProductList = ProductCrud.GetAll();
            mainGrid.ItemsSource = ProductList;
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
                Product model = (Product)e.EditedItem;

                ProductCrud.Edit(model);

                for (int i = 0; i < ProductList.Count; i++)
                {
                    if (ProductList[i].Id == model.Id)
                    {
                        ProductList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = ProductList;
                mainGrid.Rebind();
            }
        }
    }
}
