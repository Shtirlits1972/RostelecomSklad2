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
    public partial class ReportsEdit : Window
    {
        public bool IsEdit = false;
        public Reports model = new Reports();
        public ReportsEdit()
        {
            InitializeComponent();
            picCreationDate.SelectedDate = DateTime.Now;
            picCreationDate.DisplayDate = DateTime.Now;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.ReportsName = txtReportsName.Text;
            model.Deskr = txtDeskr.Text;
            model.CreationDate = (DateTime)picCreationDate.SelectedDate;

            if (IsEdit)
            {
                ReportsCrud.Edit(model);
            }
            else
            {
                model = ReportsCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtReportsName.Text = model.ReportsName;
                txtDeskr.Text = model.Deskr;

                picCreationDate.SelectedDate = model.CreationDate;
                picCreationDate.DisplayDate = model.CreationDate;
            }
        }
    }
}
//           <!--ReportsName, Deskr, CreationDate-->