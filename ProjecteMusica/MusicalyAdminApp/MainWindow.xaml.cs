using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.Taules;
using MusicalyAdminApp.View;

namespace MusicalyAdminApp
{
    /// <summary>
    /// MainWindow class represents the main window of the MusicalyAdminApp.
    /// </summary>
    public partial class MainWindow : Window
    {
        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            if (IsLoaded)
            {
                string path = @"./afasfa";
                /*if (!Directory.Exists(path))
                {*/
                    ChooseDockerAndApi cda = new ChooseDockerAndApi();
                    cda.Show();
                    Close();
                /*}
                else
                {*/
                    apiSql = new Apisql();
                    // Initialize and display songs and albums.
                    ShowSongs();
                    ShowAlbums();
                    WindowState = WindowState.Maximized;
                //}
            }
        }

        /// <summary>
        /// Method to asynchronously retrieve and display songs.
        /// </summary>
        private async Task ShowSongs()
        {
            try
            {
                // Get the list of songs from the API.
                List<Song> songs = await apiSql.GetSongs();
                ListBoxCanciones.ItemsSource = songs;
                ListBoxCanciones.SelectionChanged += ListBoxCanciones_SelectionChanged;
                Inf.SaveClicked += SongInfo_SaveClicked;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting and displaying songs: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for song selection change in the ListBox.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBoxCanciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Display song information when a song is selected.
            Song selectedSong = ListBoxCanciones.SelectedItem as Song;
            if (selectedSong != null)
            {
                Inf.UIDInf.Text = $"{selectedSong.UID}";
                Inf.NomInfTextBox.Text = $"{selectedSong.Title}";
                Inf.IdiomaInfTextBox.Text = $"{selectedSong.Language}";
                Inf.DuracioInfTextBox.Text = $"{selectedSong.Duration}";
            }
        }

        /// <summary>
        /// Event handler for the Save button click in the SongInfo UserControl.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void SongInfo_SaveClicked(object sender, EventArgs e)
        {
            int Durationint;

            try
            {
                int.TryParse(Inf.DurationInf.Text, out Durationint);
                Song selectedSong = ListBoxCanciones.SelectedItem as Song;

                if (selectedSong != null)
                {
                    selectedSong.Title = Inf.NameInf.Text;
                    selectedSong.Language = Inf.LangInf.Text;
                    selectedSong.Duration = Durationint;
                    using (var apiSql = new Apisql())
                    {
                        Extension extensionSong = new Extension();
                        extensionSong.Name = Inf.FormatInf.Text;
                        selectedSong.Extensions = new List<Extension> { extensionSong };
                        string uidString = selectedSong.UID.ToString();
                        string updateResponse = await apiSql.UpdateSong(uidString, selectedSong);
                        Console.WriteLine(updateResponse);
                    }
                    
                    ListBoxCanciones.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the edited song: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for the window closed event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            apiSql.Dispose();
        }

        /// <summary>
        /// Event handler for the Search button click.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void btnSearch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string uid = SrchBar.Text;
            if (uid == null)
            {
                ShowSongs();
            }
            else
            {
                List<Song> songs = await apiSql.GetSong(uid);
                ListBoxCanciones.ItemsSource = songs;
                ListBoxCanciones.SelectionChanged += ListBoxCanciones_SelectionChanged;
                Inf.SaveClicked += SongInfo_SaveClicked;
            }
        }

        /// <summary>
        /// Method to asynchronously retrieve and display albums.
        /// </summary>
        private async Task ShowAlbums()
        {
            try
            {
                List<Album> albums = await apiSql.GetAlbums();
                ListBoxAlbums.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting and displaying albums: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler for the Generate PDF button click.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            PdfView pdfView = new PdfView();
            pdfView.Show();
        }
        
        /// <summary>
        /// Event handler for the "Album" button click.
        /// Opens the ViewAlbum window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAlbum_Click(object sender, RoutedEventArgs e)
        {
            ViewAlbum viewAlbum = new ViewAlbum();
            viewAlbum.Show();
        }

        /// <summary>
        /// Event handler for the "Bands" button click.
        /// Opens the ViewBands window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnBandas_Click(object sender, RoutedEventArgs e)
        {
            ViewBands viewBands = new ViewBands();
            viewBands.Show();
        }

        /// <summary>
        /// Event handler for the "Musicians" button click.
        /// Opens the ViewMusician window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnMusicians_Click(object sender, RoutedEventArgs e)
        {
            ViewMusician viewMusicians = new ViewMusician();
            viewMusicians.Show();
        }

        /// <summary>
        /// Event handler for the "Instruments" button click.
        /// Opens the ViewInstrument window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnInstruments_Click(object sender, RoutedEventArgs e)
        {
            ViewInstrument viewInstruments = new ViewInstrument();
            viewInstruments.Show();
        }

        /// <summary>
        /// Event handler for the "Historics" button click.
        /// Opens the ViewHistoric window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnHistorics_Click(object sender, RoutedEventArgs e)
        {
            ViewHistoric viewHistoric = new ViewHistoric();
            viewHistoric.Show();
        }

        /// <summary>
        /// Event handler for the "Up Song" button click.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnUpSong_Click(object sender, RoutedEventArgs e)
        {
            ViewUpSong viewUpSong = new ViewUpSong();
            viewUpSong.Show();
        }

        /// <summary>
        /// Event handler for the "Llistat Generat" button click.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnLlistatGenerat_Click(object sender, RoutedEventArgs e)
        {
            PdfView pdfView = new PdfView();
            pdfView.Show();
        }
    }
}