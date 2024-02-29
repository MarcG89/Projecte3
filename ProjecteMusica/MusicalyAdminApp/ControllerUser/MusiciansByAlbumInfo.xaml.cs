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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicalyAdminApp.ControllerUser
{
    /// <summary>
    /// Lógica de interacción para MusiciansByAlbumInfo.xaml
    /// </summary>
    public partial class MusiciansByAlbumInfo : UserControl
    {
        private Apisql apisql;
        private List<Album> albums;
        private List<Song> cancons;
        private List<Play> Plays;
        private List<Musician> Musicians;
        private List<string> titolsAlbums;

        public MusiciansByAlbumInfo()
        {
            InitializeComponent();
            this.ObtenirAlbums();
        }

        /// <summary>
        /// Guarda els titols dels Albums juntament amb les seves cancons 
        /// afagant-los de l'array d'Albums
        /// </summary>
        private async void ObtenirDadesAlbums()
        {
            for(int i = 0; i < this.albums.Count; i++)
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
        private async void ObtenirAlbums()
        {
            this.albums = await this.apisql.GetAlbums();
            this.ObtenirDadesAlbums();
            this.ComboBoxAlbums.ItemsSource = this.titolsAlbums;
            
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxAlbums.selectedItem != null)
            {

            }
        }
    }
}
