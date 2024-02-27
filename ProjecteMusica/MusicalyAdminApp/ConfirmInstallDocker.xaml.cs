using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MusicalyAdminApp
{
    /// <summary>
    /// Lógica de interacción para ConfirmInstallDocker.xaml
    /// </summary>
    public partial class ConfirmInstallDocker : Window
    {
        public ConfirmInstallDocker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Si l'usuari vol instal·lar el Docker, s'executarà el document .exe
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void confirm(object sender, RoutedEventArgs e)
        {
            Process.Start("docker-installer.exe");
            Close();
        }

        /// <summary>
        /// Si l'usuari no vol instal·lar el Docker, es tancarà la finestra actual
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void closeWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
