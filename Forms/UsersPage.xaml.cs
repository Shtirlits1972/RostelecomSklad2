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
    public partial class UsersPage : Page
    {
        List<Users> UsersList = new List<Users>();
        public UsersPage()
        {
            InitializeComponent();
            colRole.ItemsSource = UsersCrud.RoleArr;
        }
        //                       <!--UsersName, Login, Password, Role-->
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UsersEdit edit = new UsersEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Users model = edit.model;

            if (model != null && model.Id > 0)
            {
                UsersList.Add(model);

                mainGrid.ItemsSource = UsersList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                UsersEdit edit = new UsersEdit();
                edit.IsEdit = true;
                Users model = (Users)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < UsersList.Count; i++)
                {
                    Users item = UsersList[i];

                    if (model.Id == item.Id)
                    {
                        UsersList[i] = model;
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
                    Users model = (Users)mainGrid.SelectedItem;
                    UsersCrud.Del(model.Id);

                    UsersList.Remove(model);

                    mainGrid.ItemsSource = UsersList;
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
            UsersList = UsersCrud.GetAll();
            mainGrid.ItemsSource = UsersList;
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
                Users model = (Users)e.EditedItem;

                UsersCrud.Edit(model);

                for (int i = 0; i < UsersList.Count; i++)
                {
                    if (UsersList[i].Id == model.Id)
                    {
                        UsersList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = UsersList;
                mainGrid.Rebind();
            }
        }
    }
}
