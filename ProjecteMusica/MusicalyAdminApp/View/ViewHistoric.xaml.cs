using MusicalyAdminApp.API.APISQL.Taules;
using MusicalyAdminApp.API.APISQL;
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

namespace MusicalyAdminApp.View
{
    /// <summary>
    /// Interaction logic for ViewHistoric.xaml
    /// Represents the window for viewing historical records.
    /// </summary>
    public partial class ViewHistoric : Window
    {
        // API instance for historical records operations.
        private readonly ApiHistoric apiHistoric;

        /// <summary>
        /// Constructor for the ViewHistoric class.
        /// Initializes the window and displays the list of historical records.
        /// </summary>
        public ViewHistoric()
        {
            InitializeComponent();
            apiHistoric = new ApiHistoric();
            ShowHistoric();
        }

        /// <summary>
        /// Asynchronously retrieves and displays a list of historical records using the API_Historic.
        /// </summary>
        private async Task ShowHistoric()
        {
            try
            {
                // Attempt to retrieve a list of albums from the API_SQL asynchronously
                List<Historial> historial = await apiHistoric.GetHistorial();

                // Set the retrieved albums as the item source for ListBoxAlbums
                ListBoxHistorics.ItemsSource = historial;
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception while getting and displaying albums
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }
    }
}
