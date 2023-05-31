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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace RostelecomSklad2.Forms
{
    public partial class OrdersPage : Page
    {
        List<Orders> OrdersList = new List<Orders>();
        List<OrderDetail> orderDetails = new List<OrderDetail>();
        public OrdersPage()
        {
            InitializeComponent();
            colUsers.ItemsSource = UsersCrud.GetAll();
            colOrdersStatus.ItemsSource = OrdersCrud.OrdersStatusList;
            colProduct.ItemsSource = ProductCrud.GetAll();

            detailGrid.ItemsSource = orderDetails;
            detailGrid.Rebind();
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

                    detailGrid.ItemsSource = null;
                    detailGrid.Rebind();

                    if (mainGrid.Items != null && mainGrid.Items.Count > 0)
                    {
                        var selObj = mainGrid.Items[0];
                        mainGrid.SelectedItem = selObj;
                    }
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

            if (mainGrid.Items != null && mainGrid.Items.Count > 0)
            {
                var selObj = mainGrid.Items[0];
                mainGrid.SelectedItem = selObj;
            }
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

        private void btnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                Orders orders = (Orders)mainGrid.SelectedItem;

                OrderDetailEdit orderDetailEdit = new OrderDetailEdit();
                orderDetailEdit.IsEdit = false;
                orderDetailEdit.model.OrderId = orders.Id;

                orderDetailEdit.ShowDialog();
                OrderDetail orderDetail = orderDetailEdit.model;

                if (orderDetail != null && orderDetail.Id > 0)
                {
                    orderDetails.Add(orderDetail);
                    detailGrid.ItemsSource = orderDetails;
                    detailGrid.Rebind();
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnEditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (detailGrid.SelectedItem != null)
            {
                OrderDetailEdit edit = new OrderDetailEdit();
                edit.IsEdit = true;
                OrderDetail model = (OrderDetail)detailGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                if (model != null && model.Id > 0)
                {
                    for (int i = 0; i < orderDetails.Count; i++)
                    {
                        OrderDetail item = orderDetails[i];

                        if (model.Id == item.Id)
                        {
                            orderDetails[i] = model;
                            break;
                        }
                    }
                    detailGrid.Rebind();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }

        private void btnDelDetail_Click(object sender, RoutedEventArgs e)
        {
            if (detailGrid.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    OrderDetail orderDetail = (OrderDetail)detailGrid.SelectedItem;
                    OrderDetailCrud.Del(orderDetail.Id);

                    orderDetails.Remove(orderDetail);
                    detailGrid.ItemsSource = orderDetails;
                    detailGrid.Rebind();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }

        private void mainGrid_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                Orders model = (Orders)mainGrid.SelectedItem;
                orderDetails = OrderDetailCrud.GetAll(model.Id);
                detailGrid.ItemsSource = orderDetails;
                detailGrid.Rebind();
            }
        }

        private void detailGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                OrderDetail orderDetail = (OrderDetail)e.EditedItem;
                OrderDetailCrud.Edit(orderDetail);

                for (int i = 0; i < orderDetails.Count; i++)
                {
                    if (orderDetails[i].Id == orderDetail.Id)
                    {
                        orderDetails[i] = orderDetail;
                        detailGrid.ItemsSource = orderDetails;
                        detailGrid.Rebind();
                        break;
                    }
                }
            }
        }
    }
}
