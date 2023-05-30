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
    public partial class OrdersEdit : Window
    {
        public bool IsEdit = false;
        public Orders model = new Orders();
        public OrdersEdit()
        {
            InitializeComponent();

            comboUsers.ItemsSource = UsersCrud.GetAll();
            comboUsers.SelectedIndex = 0;

            comboOrdersStatus.ItemsSource = OrdersCrud.OrdersStatusList;
            comboOrdersStatus.SelectedIndex = 0;

            picOrderDate.SelectedDate = DateTime.Now;
            picOrderDate.DisplayDate = DateTime.Now;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.OrdersNumber = txtOrdersNumber.Text;
            model.OrderDate = (DateTime)picOrderDate.SelectedDate;

            Users users = (Users)comboUsers.SelectedItem;
            model.UsersId = users.Id;
            model.UsersName = users.UsersName;

            model.OrdersStatus = (string)comboOrdersStatus.SelectedItem;

            if (IsEdit)
            {
                OrdersCrud.Edit(model);
            }
            else
            {
                model = OrdersCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtOrdersNumber.Text = model.OrdersNumber;

                picOrderDate.SelectedDate =  model.OrderDate;
                picOrderDate.DisplayDate = model.OrderDate;

                comboUsers.SelectedItem = new Users {Id = model.UsersId , UsersName = model.UsersName};

                comboOrdersStatus.SelectedItem = model.OrdersStatus;
            }
        }
    }
}
