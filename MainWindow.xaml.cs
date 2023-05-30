using RostelecomSklad2.Forms;
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

namespace RostelecomSklad2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizationManager.Manager = new CustomLocalizationManager();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ProductPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/ProductPage.xaml", UriKind.Relative));
            Title = "Товары";
        }

        private void UsersPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/UsersPage.xaml", UriKind.Relative));
            Title = "Пользователи";
        }

        private void SupplayPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/SupplayPage.xaml", UriKind.Relative));
            Title = "Поставщики";
        }

        private void ReportsPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/ReportsPage.xaml", UriKind.Relative));
            Title = "Отчёты";
        }

        private void DepartmentsPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/DepartmentsPage.xaml", UriKind.Relative));
            Title = "Отделы";
        }

        private void EmployeePageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/EmployeePage.xaml", UriKind.Relative));
            Title = "Сотрудники";
        }

        private void WarehousePageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/WarehousePage.xaml", UriKind.Relative));
            Title = "Склады";
        }

        private void LoginLogPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/LoginLogPage.xaml", UriKind.Relative));
            Title = "LoginLog";
        }

        private void DeliveriesPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/DeliveriesPage.xaml", UriKind.Relative));
            Title = "Доставки";
        }

        private void OrdersPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/OrdersPage.xaml", UriKind.Relative));
            Title = "Заказы";
        }
    }
}
