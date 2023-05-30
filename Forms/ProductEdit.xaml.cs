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
    public partial class ProductEdit : Window
    {
        public bool IsEdit = false;
        public Product model = new Product();
        public ProductEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.ProductName = txtProductName.Text;
            model.Specifications = txtSpecifications.Text;

            decimal decPrice = 0;
            decimal.TryParse(txtPrice.Text, out decPrice);
            model.Price = decPrice;

            int intQty = 0;
            int.TryParse(txtQty.Text, out intQty);
            model.Qty = intQty;

            if (IsEdit)
            {
                ProductCrud.Edit(model);
            }
            else
            {
                model = ProductCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtProductName.Text = model.ProductName;
                txtSpecifications.Text = model.Specifications;
                txtPrice.Text = model.Price.ToString();
                txtQty.Text = model.Qty.ToString();
            }
        }
    }
}
