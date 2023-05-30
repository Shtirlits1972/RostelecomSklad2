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

namespace RostelecomSklad2.Forms
{
    public partial class OrderDetailEdit : Window
    {
        public bool IsEdit = false;
        public OrderDetail model = new OrderDetail();
        public OrderDetailEdit()
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

            int intQty = 0;
            int.TryParse(txtQty.Text, out intQty);
            model.Qty = intQty;

            Product product = (Product)comboProduct.SelectedItem;
            model.ProductId = product.Id;
            model.ProductName = product.ProductName;

            if (IsEdit)
            {
                OrderDetailCrud.Edit(model);
            }
            else
            {
                model = OrderDetailCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                comboProduct.SelectedItem = new Product { Id = model.ProductId, ProductName = model.ProductName };
                txtQty.Text = model.Qty.ToString();
            }
        }
    }
}
