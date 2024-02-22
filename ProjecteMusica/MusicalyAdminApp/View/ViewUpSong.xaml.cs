using Microsoft.Win32;
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
    /// Interaction logic for ViewUpSong.xaml
    /// Represents the window for uploading new songs.
    /// </summary>
    public partial class ViewUpSong : Window
    {
        // Path to the selected music file.
        private string pathMusic = "";

        // API instance for audio-related operations.
        private readonly ApiAudio apiAudio;

        // API instance for SQL operations.
        private readonly Apisql apiSql;

        /// <summary>
        /// Constructor for the ViewUpSong class.
        /// Initializes the window and API instances.
        /// </summary>
        public ViewUpSong()
        {
            InitializeComponent();
            apiAudio = new ApiAudio();
            apiSql = new Apisql();
        }

        /// <summary>
        /// Event handler for the BrowseButton click.
        /// Opens a file dialog to select an audio file.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.flac|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
                pathMusic = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Event handler for the SubmitButton click.
        /// Uploads a new song and its associated audio file to the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            SongPostModel song = new SongPostModel
            {
                UID = Guid.NewGuid().ToString(),
                Title = SongNameTextBox.Text
            };

            apiSql.PostSong(song);
            apiAudio.PostAudio(song.UID, pathMusic);
        }
    }
}
