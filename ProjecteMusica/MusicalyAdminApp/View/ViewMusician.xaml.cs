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
    /// Interaction logic for ViewMusician.xaml
    /// Represents the window for viewing and editing musician information.
    /// </summary>
    public partial class ViewMusician : Window
    {
        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for the ViewMusician class.
        /// Initializes the window and displays the list of musicians.
        /// </summary>
        public ViewMusician()
        {
            InitializeComponent();
            apiSql = new Apisql();
            ShowMusicians();
        }

        /// <summary>
        /// Asynchronously retrieves and displays a list of musicians using the API_SQL.
        /// </summary>
        private async Task ShowMusicians()
        {
            try
            {
                // Attempt to retrieve a list of albums from the API_SQL asynchronously
                List<Musician> musicians = await apiSql.GetMusicians();

                // Set the retrieved albums as the item source for ListBoxAlbums
                ListBoxMusician.ItemsSource = musicians;

                ListBoxMusician.SelectionChanged += ListBoxMusicians_SelectionChanged;

                InfMusician.SaveClicked += MusicianInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception while getting and displaying albums
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for musician selection change in the ListBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxMusicians_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Musician selectedMusician = ListBoxMusician.SelectedItem as Musician;
            if (selectedMusician != null)
            {
                InfMusician.NameMusicianInf.Text = $"{selectedMusician.Name}";
                InfMusician.AgeMusicianInf.Text = $"{selectedMusician.Age}";
            }
        }

        /// <summary>
        /// Event handler for the Save button click in the InfMusician UserControl.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void MusicianInfo_SaveClicked(object sender, EventArgs e)
        {
            try
            {
                Musician selectedMusician = ListBoxMusician.SelectedItem as Musician;
                int ageInt;

                if (selectedMusician != null)
                {
                    int.TryParse(InfMusician.AgeMusicianInf.Text, out ageInt);
                    selectedMusician.Name = InfMusician.NameMusicianInf.Text;
                    selectedMusician.Age = ageInt;

                    using (var apiSql = new Apisql())
                    {
                        await apiSql.UpdateMusician(selectedMusician.Name, selectedMusician);
                    }

                    // Actualizar el ListBox después de la modificación
                    ListBoxMusician.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }
    }
}
