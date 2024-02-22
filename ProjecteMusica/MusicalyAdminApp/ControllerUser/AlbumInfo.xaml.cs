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
    /// Constructor of the AlbumInfo class.
    /// </summary>
    public partial class AlbumInfo : UserControl
    {
        public AlbumInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered when the "Save" button is clicked.
        /// </summary>
        public event EventHandler SaveClicked;

        /// <summary>
        /// Toggle the read-only status of the textboxes when the "Edit" 
        /// button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NameAlbumInf.IsReadOnly = !NameAlbumInf.IsReadOnly;
            YearAlbumInf.IsReadOnly = !YearAlbumInf.IsReadOnly;
        }

        /// <summary>
        /// Trigger the SaveClicked event when the "Save" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveClicked?.Invoke(this, EventArgs.Empty);
        }


        /// <summary>
        /// Getters for accessing the textboxes.
        /// </summary>
        public TextBox NameAlbumInfTextBox => NameAlbumInf;
        public TextBox YearAlbumInfTextBox => YearAlbumInf;
    }
}
