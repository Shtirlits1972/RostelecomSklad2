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
    public partial class WarehouseEdit : Window
    {
        public bool IsEdit = false;
        public Warehouse model = new Warehouse();
        public WarehouseEdit()
        {
            InitializeComponent();
            comboProduct.ItemsSource = ProductCrud.GetAll();
            comboProduct.SelectedIndex = 0; 
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.WarehouseName = txtWarehouseName.Text;

            int intQty = 0;
            int.TryParse(txtProductQty.Text, out intQty);

            model.ProductQty = intQty;
            Product product = (Product)comboProduct.SelectedItem;

            model.ProductId = product.Id;
            model.ProductName = product.ProductName;

            if (IsEdit)
            {
                WarehouseCrud.Edit(model);
            }
            else
            {
                model = WarehouseCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtWarehouseName.Text = model.WarehouseName;
                txtProductQty.Text = model.ProductQty.ToString();

                comboProduct.SelectedItem  =  new Product { Id = model.ProductId, ProductName = model.ProductName };
            }
        }
    }
}
