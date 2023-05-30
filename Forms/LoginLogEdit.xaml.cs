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
    public partial class LoginLogEdit : Window
    {
        public bool IsEdit = false;
        public LoginLog model = new LoginLog();
        public LoginLogEdit()
        {
            InitializeComponent();

            picLoginDateTime.SelectedDate = DateTime.Now;
            picLoginDateTime.DisplayDate = DateTime.Now;

            comboUsers.ItemsSource = UsersCrud.GetAll();
            comboUsers.SelectedIndex = 0;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.LoginDateTime = (DateTime)picLoginDateTime.SelectedDate;

            Users users = (Users)comboUsers.SelectedItem;
            model.UsersId = users.Id;
            model.UsersName = users.UsersName;

            if (IsEdit)
            {
                LoginLogCrud.Edit(model);
            }
            else
            {
                model = LoginLogCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                comboUsers.SelectedItem = new Users { Id = model.UsersId, UsersName = model.UsersName };
                picLoginDateTime.SelectedDate = model.LoginDateTime;
                picLoginDateTime.DisplayDate = model.LoginDateTime;
            }
        }
    }
}
