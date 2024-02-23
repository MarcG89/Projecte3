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
    /// Represents the window for viewing and editing albums.
    /// </summary>
    public partial class ViewAlbum : Window
    {
        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for the ViewAlbum class.
        /// Initializes the window and displays the list of albums.
        /// </summary>
        public ViewAlbum()
        {
            InitializeComponent();
            apiSql = new Apisql();
            ShowAlbums();
        }

        /// <summary>
        /// Method to asynchronously retrieve and display albums.
        /// </summary>
        private async Task ShowAlbums()
        {
            try
            {
                // Attempt to retrieve a list of albums from the API_SQL asynchronously
                List<Album> albums = await apiSql.GetAlbums();

                // Set the retrieved albums as the item source for ListBoxAlbums
                ListBoxAlbums.ItemsSource = albums;

                ListBoxAlbums.SelectionChanged += ListBoxAlbums_SelectionChanged;

                InfAlbum.SaveClicked += AlbumInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception while getting and displaying albums
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for album selection change in the ListBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Album selectedAlbum = ListBoxAlbums.SelectedItem as Album;
            if (selectedAlbum != null)
            {
                InfAlbum.NameAlbumInf.Text = $"{selectedAlbum.AlbumName}";
                InfAlbum.YearAlbumInf.Text = $"{selectedAlbum.Year}";
            }
        }

        /// <summary>
        /// Event handler for the Save button click in the AlbumInfo UserControl.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void AlbumInfo_SaveClicked(object sender, EventArgs e)
        {
            int yearInt;

            try
            {
                int.TryParse(InfAlbum.YearAlbumInf.Text, out yearInt);
                Album selectedAlbum = ListBoxAlbums.SelectedItem as Album;

                if (selectedAlbum != null)
                {
                    selectedAlbum.AlbumName = InfAlbum.NameAlbumInf.Text;
                    selectedAlbum.Year = yearInt;
                    using (var apiSql = new Apisql())
                    {
                        await apiSql.UpdateAlbum(selectedAlbum.AlbumName, selectedAlbum);
                    }

                    // Actualizar el ListBox después de la modificación
                    ListBoxAlbums.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }
    }
}
