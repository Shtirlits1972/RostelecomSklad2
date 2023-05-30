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
    public partial class UsersEdit : Window
    {
        public bool IsEdit = false;
        public Users model = new Users();
        public UsersEdit()
        {
            InitializeComponent();
            comboRole.ItemsSource = UsersCrud.RoleArr; 
            comboRole.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.UsersName = txtUsersName.Text;
            model.Login = txtLogin.Text;
            model.Password = txtPassword.Text;
            model.Role = ((string)comboRole.SelectedItem);

            if (IsEdit)
            {
                UsersCrud.Edit(model);
            }
            else
            {
                model = UsersCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtUsersName.Text = model.UsersName;
                txtLogin.Text = model.Login;
                txtPassword.Text = model.Password;

                #region Role
                for (int i = 0; i < comboRole.Items.Count; i++)
                {
                    string Role = (string)comboRole.Items[i];
                    if (Role == model.Role)
                    {
                        comboRole.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
