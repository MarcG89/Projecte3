using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.Taules;
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
    /// Interaction logic for ViewBands.xaml
    /// Represents the window for viewing and editing bands.
    /// </summary>
    public partial class ViewBands : Window
    {
        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for the ViewBands class.
        /// Initializes the window and displays the list of bands.
        /// </summary>
        public ViewBands()
        {
            InitializeComponent();
            apiSql = new Apisql();
            ShowBands();
        }

        /// <summary>
        /// Asynchronously retrieves and displays a list of bands using the API_SQL.
        /// </summary>
        private async Task ShowBands()
        {
            try
            {
                // Attempt to retrieve a list of albums from the API_SQL asynchronously
                List<Band> bands = await apiSql.GetBands();

                // Set the retrieved albums as the item source for ListBoxAlbums
                ListBoxBands.ItemsSource = bands;

                ListBoxBands.SelectionChanged += ListBoxBands_SelectionChanged;

                InfBand.SaveClicked += BandInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception while getting and displaying albums
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for band selection change in the ListBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxBands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Band selectedBand = ListBoxBands.SelectedItem as Band;
            if (selectedBand != null)
            {
                InfBand.NameBandInf.Text = $"{selectedBand.Name}";
                InfBand.OriginBandInf.Text = $"{selectedBand.Origin}";
                InfBand.GenereBandInf.Text = $"{selectedBand.Genre}";
            }
        }

        /// <summary>
        /// Event handler for the Save button click in the InfBand UserControl.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void BandInfo_SaveClicked(object sender, EventArgs e)
        {
            int yearInt;

            try
            {
                Band selectedBand = ListBoxBands.SelectedItem as Band;

                if (selectedBand != null)
                {
                    selectedBand.Name = InfBand.NameBandInf.Text;
                    selectedBand.Origin = InfBand.OriginBandInf.Text;
                    selectedBand.Genre = InfBand.GenereBandInf.Text;

                    using (var apiSql = new Apisql())
                    {

                        await apiSql.UpdateBand(selectedBand.Name, selectedBand);
                    }

                    // Actualizar el ListBox después de la modificación
                    ListBoxBands.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }
    }
}
