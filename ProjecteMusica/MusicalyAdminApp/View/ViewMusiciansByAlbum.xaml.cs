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
    /// Lógica de interacción para ViewMusiciansByAlbum.xaml
    /// </summary>
    public partial class ViewMusiciansByAlbum : Window
    {
        private Apisql apisql;
        private List<Album> albums;
        private SongOriginal canconsOriginals;
        private List<Song> cancons;
        private List<SongListView> canconsListView;
        private List<Play> Plays;
        private List<Musician> Musicians;
        private List<string> titolsAlbums;

        public ViewMusiciansByAlbum()
        {
            InitializeComponent();
            this.apisql = new Apisql();
        }

        /// <summary>
        /// Guarda els titols dels Albums juntament amb les seves cancons 
        /// afagant-los de l'array d'Albums
        /// </summary>
        private async void ObtenirDadesAlbums()
        {
            for (int i = 0; i < this.albums.Count; i++)
            {
                this.titolsAlbums.Add(this.albums[i].Titol);

                for (int y = 0; y < this.albums[i].Songs.Count; y++)
                {
                    this.cancons.Add(this.albums[i].Songs.ToList()[y]);
                }
            }
        }

        /// <summary>
        /// Per cada Song dins l'array de Songs, creem una SongListView que haurem
        /// d'afegir a la ListView "songListView"
        /// </summary>
        private async void SongsToSongsListView()
        {
            for (int i = 0; i < this.cancons.Count; i++)
            {
                SongListView songListView = new SongListView();
                songListView.UID = this.cancons[i].UID;
                songListView.Title = this.cancons[i].Title;
                songListView.Language = this.cancons[i].Language;
                songListView.Duration = this.cancons[i].Duration;

                this.canconsListView.Add(
                    songListView
                );

            }
        }

        /// <summary>
        /// Guarda les Cancons d'un Album fent clic a al botó de Search
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void btnObtenirCanconsAlbums_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.InfMusiciansByAlbum.NameAlbumInf.Text))
            {
                this.canconsOriginals = await this.apisql.GetSongsAlbumByName(this.InfMusiciansByAlbum.NameAlbumInf.Text);
                this.cancons = this.canconsOriginals.values;
                this.SongsToSongsListView();
                this.songListView.ItemsSource = this.canconsListView;
            }
            else
            {
                MessageBox.Show("Nom de l'Àlbum invàlid");
            }
        }
    }
}
