﻿using RostelecomSklad2.CRUD;
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
    public partial class DepartmentsEdit : Window
    {
        public bool IsEdit = false;
        public Departments model = new Departments();
        public DepartmentsEdit()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.DepartmentsName = txtDepartmentsName.Text;
            model.Deskr = txtDeskr.Text;

            if (IsEdit)
            {
                DepartmentsCrud.Edit(model);
            }
            else
            {
                model = DepartmentsCrud.Add(model);
            }
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtDepartmentsName.Text = model.DepartmentsName;
                txtDeskr.Text = model.Deskr;
            }
        }
    }
}
