﻿using System;
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

namespace MusicalyAdminApp
{
    /// <summary>
    /// Lógica de interacción para MenuDocker.xaml
    /// </summary>
    public partial class MenuDocker : UserControl
    {
        public MenuDocker()
        {
            InitializeComponent();
            List<string> options = new List<string>();
            options.Add("MSSQL");
            options.Add("MYSQL");
            options.Add("PostgreSQL");
            this.opcionsDocker.ItemsSource = options;
        }
    }
}
