using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace AppSetupFile
{
    /// <summary>
    /// Lógica de interacción para DbDocker.xaml
    /// </summary>
    public partial class DbDocker : Window
    {
        public DbDocker()
        {
            InitializeComponent();
            List<string> options = new List<string>();
            options.Add("MSSQL");
            options.Add("MySQL");
            options.Add("PostgreSQL");
            this.opcionsDocker.ItemsSource = options;
        }

        public void checkDirectory()
        {
            string path = @"C:\Program Files\Docker";
            if (!Directory.Exists(path))
            {
                using (WebClient wc = new WebClient())
                {
                    //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new System.Uri("http://www.sayka.com/downloads/front_view.jpg"),
                        // Param2 = Path to save
                        "D:\\Images\\front_view.jpg"
                    );
                }
            }
        }
    }
}
