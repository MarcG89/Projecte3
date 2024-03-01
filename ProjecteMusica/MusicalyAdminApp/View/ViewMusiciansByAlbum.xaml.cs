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
        /// Guarda tots els Albums fent una crida a l'API
        /// </summary>
        private async void ObtenirCanconsAlbums()
        {
            if (!string.IsNullOrEmpty(this.InfMusiciansByAlbum.NameAlbumInf.Text))
            {
                this.cancons = await this.apisql.GetSongsAlbumByName(this.InfMusiciansByAlbum.NameAlbumInf.Text);
                this.ObtenirDadesAlbums();
            }
            else
            {
                MessageBox.Show("Nom de l'Àlbum invàlid");
            }
        }

        /// <summary>
        /// Toggle the read-only status of the textboxes when the "Edit" 
        /// button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.InfMusiciansByAlbum.NameAlbumInf.IsReadOnly = !this.InfMusiciansByAlbum.NameAlbumInf.IsReadOnly;
            this.InfMusiciansByAlbum.YearAlbumInf.IsReadOnly = !this.InfMusiciansByAlbum.YearAlbumInf.IsReadOnly;
        }
    }
}
