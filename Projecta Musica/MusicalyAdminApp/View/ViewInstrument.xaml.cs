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
    /// Interaction logic for ViewInstrument.xaml
    /// Represents the window for viewing and editing musical instruments.
    /// </summary>
    public partial class ViewInstrument : Window
    {
        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for the ViewInstrument class.
        /// Initializes the window and displays the list of musical instruments.
        /// </summary>
        public ViewInstrument()
        {
            InitializeComponent();
            apiSql = new Apisql();
            ShowInstruments();
        }

        /// <summary>
        /// Asynchronously retrieves and displays a list of musical instruments using the API_SQL.
        /// </summary>
        private async Task ShowInstruments()
        {
            try
            {
                // Attempt to retrieve a list of albums from the API_SQL asynchronously
                List<Instrument> instruments = await apiSql.GetInstruments();

                // Set the retrieved albums as the item source for ListBoxAlbums
                ListBoxInstruments.ItemsSource = instruments;

                ListBoxInstruments.SelectionChanged += ListBoxInstruments_SelectionChanged;

                InfInstrument.SaveClicked += InstrumentInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception while getting and displaying albums
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for instrument selection change in the ListBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxInstruments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Instrument selectedInstrument = ListBoxInstruments.SelectedItem as Instrument;
            if (selectedInstrument != null)
            {
                InfInstrument.NameInstrumentInf.Text = $"{selectedInstrument.Name}";
                InfInstrument.TypeInstrumentInf.Text = $"{selectedInstrument.Type}";
            }
        }

        /// <summary>
        /// Event handler for the Save button click in the InfInstrument UserControl.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void InstrumentInfo_SaveClicked(object sender, EventArgs e)
        {
            int yearInt;

            try
            {
                Instrument selectedInstrument = ListBoxInstruments.SelectedItem as Instrument;

                if (selectedInstrument != null)
                {
                    selectedInstrument.Name = InfInstrument.NameInstrumentInf.Text;
                    selectedInstrument.Type = InfInstrument.TypeInstrumentInf.Text;

                    using (var apiSql = new Apisql())
                    {

                        await apiSql.UpdateInstrument(selectedInstrument.Name, selectedInstrument);
                    }

                    // Actualizar el ListBox después de la modificación
                    ListBoxInstruments.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }
    }
}
