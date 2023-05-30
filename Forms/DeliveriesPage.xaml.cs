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
    public partial class DeliveriesPage : Page
    {
        List<Deliveries> DeliveriesList = new List<Deliveries>();
        public DeliveriesPage()
        {
            InitializeComponent();
            colProduct.ItemsSource = ProductCrud.GetAll();
            colSupplay.ItemsSource = SupplayCrud.GetAll();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DeliveriesEdit edit = new DeliveriesEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Deliveries model = edit.model;

            if (model != null && model.Id > 0)
            {
                DeliveriesList.Add(model);

                mainGrid.ItemsSource = DeliveriesList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                DeliveriesEdit edit = new DeliveriesEdit();
                edit.IsEdit = true;
                Deliveries model = (Deliveries)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < DeliveriesList.Count; i++)
                {
                    Deliveries item = DeliveriesList[i];

                    if (model.Id == item.Id)
                    {
                        DeliveriesList[i] = model;
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
                    Deliveries model = (Deliveries)mainGrid.SelectedItem;
                    DeliveriesCrud.Del(model.Id);

                    DeliveriesList.Remove(model);

                    mainGrid.ItemsSource = DeliveriesList;
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
            DeliveriesList = DeliveriesCrud.GetAll();
            mainGrid.ItemsSource = DeliveriesList;
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
                Deliveries model = (Deliveries)e.EditedItem;

                DeliveriesCrud.Edit(model);

                for (int i = 0; i < DeliveriesList.Count; i++)
                {
                    if (DeliveriesList[i].Id == model.Id)
                    {
                        DeliveriesList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = DeliveriesList;
                mainGrid.Rebind();
            }
        }
    }
}
