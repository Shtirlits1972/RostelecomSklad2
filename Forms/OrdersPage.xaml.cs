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
    public partial class OrdersPage : Page
    {
        List<Orders> OrdersList = new List<Orders>();
        public OrdersPage()
        {
            InitializeComponent();
            colUsers.ItemsSource = UsersCrud.GetAll();
            colOrdersStatus.ItemsSource = OrdersCrud.OrdersStatusList;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OrdersEdit edit = new OrdersEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Orders model = edit.model;

            if (model != null && model.Id > 0)
            {
                OrdersList.Add(model);

                mainGrid.ItemsSource = OrdersList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                OrdersEdit edit = new OrdersEdit();
                edit.IsEdit = true;
                Orders model = (Orders)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < OrdersList.Count; i++)
                {
                    Orders item = OrdersList[i];

                    if (model.Id == item.Id)
                    {
                        OrdersList[i] = model;
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
                    Orders model = (Orders)mainGrid.SelectedItem;
                    OrdersCrud.Del(model.Id);

                    OrdersList.Remove(model);

                    mainGrid.ItemsSource = OrdersList;
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
            OrdersList = OrdersCrud.GetAll();
            mainGrid.ItemsSource = OrdersList;
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
                Orders model = (Orders)e.EditedItem;

                OrdersCrud.Edit(model);

                for (int i = 0; i < OrdersList.Count; i++)
                {
                    if (OrdersList[i].Id == model.Id)
                    {
                        OrdersList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = OrdersList;
                mainGrid.Rebind();
            }
        }
    }
}
