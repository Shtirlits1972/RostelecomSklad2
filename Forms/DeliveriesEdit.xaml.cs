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
    public partial class DeliveriesEdit : Window
    {
        public bool IsEdit = false;
        public Deliveries model = new Deliveries();
        public DeliveriesEdit()
        {
            InitializeComponent();
            comboProduct.ItemsSource = ProductCrud.GetAll();
            comboProduct.SelectedIndex = 0;

            comboSupplay.ItemsSource = SupplayCrud.GetAll();
            comboSupplay.SelectedIndex = 0;

            picDeliveryDate.SelectedDate = DateTime.Now;
            picDeliveryDate.DisplayDate = DateTime.Now;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.DeliveryDate = (DateTime)picDeliveryDate.SelectedDate;
            int intQty = 0;
            int.TryParse(txtQty.Text, out intQty);
            model.Qty = intQty;

            Product product = (Product)comboProduct.SelectedItem;
            model.ProductId = product.Id;
            model.ProductName = product.ProductName;

            Supplay supplay = (Supplay)comboSupplay.SelectedItem;
            model.SupplayId = supplay.Id;
            model.CompanyName = supplay.CompanyName;

            if (IsEdit)
            {
                DeliveriesCrud.Edit(model);
            }
            else
            {
                model = DeliveriesCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                picDeliveryDate.SelectedDate = model.DeliveryDate;
                picDeliveryDate.DisplayDate = model.DeliveryDate;

                comboProduct.SelectedItem = new Product { Id = model.ProductId, ProductName = model.ProductName };
                comboSupplay.SelectedItem = new Supplay { Id = model.SupplayId, CompanyName = model.CompanyName };

                txtQty.Text = model.Qty.ToString();
            }
        }
    }
}
