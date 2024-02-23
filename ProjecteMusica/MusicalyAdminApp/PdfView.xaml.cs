using Syncfusion.Windows.PdfViewer;
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

namespace MusicalyAdminApp
{
    /// <summary>
    /// Lógica de interacción para PdfView.xaml.
    /// Representa la ventana para visualizar archivos PDF.
    /// </summary>
    public partial class PdfView : Window
    {
        /// <summary>
        /// Constructor de la clase PdfView.
        /// Inicializa la ventana.
        /// </summary>
        public PdfView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara cuando la ventana ha sido cargada.
        /// Carga un archivo PDF en el visor al cargar la ventana.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los argumentos del evento.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string projectPath = Environment.CurrentDirectory.Replace("MusicalyAdminApp", "Server");

            // Combinar con la ruta relativa desde el directorio del servidor
            string pdfFilePath = System.IO.Path.Combine(projectPath, "ServerFitxers", "PDFsignat.pdf");

            // Cargar el PDF en el visor
            LoadPdf(pdfFilePath);
        }

        /// <summary>
        /// Carga un archivo PDF en el visor.
        /// </summary>
        /// <param name="pdfFilePath">La ruta del archivo PDF.</param>
        private void LoadPdf(string pdfFilePath)
        {
            if (!string.IsNullOrEmpty(pdfFilePath))
            {
                // Utiliza el control PdfViewerControl para cargar el PDF
                pdfViewer.Load(pdfFilePath);
            }
            else
            {
                // Muestra un mensaje o realiza alguna acción en caso de que la ruta del PDF sea nula o vacía
                MessageBox.Show("La ruta del archivo PDF no es válida.");
            }
        }
    }
}
