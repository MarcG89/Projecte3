using MusicalyAdminApp.API.APISQL.Taules;
using MusicalyAdminApp.API.APISQL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Diagnostics;

namespace MusicalyAdminApp
{
    /// <summary>
    /// Lógica de interacción para ChooseDockerAndApi.xaml
    /// </summary>
    public partial class ChooseDockerAndApi : Window
    {
        private string apiSql;
        private string apiMongoDB;
        private string apiHistorial;
        private string jsonRuta;
        private string jsonContent;
        private dynamic jsonObj;
        private string urlDownloadDocker;

        private IPAddress ipSql;
        private IPAddress ipMongoDB;
        private IPAddress ipHistorial;
        public ChooseDockerAndApi()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// Mètode per obrir la finestra principal o MainWindow
        /// </summary>
        private async void openMainWindow()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        /// <summary>
        /// Funció per comprovar si tens el docker instal·lat executant la comanda
        /// "docker version" mitjançant un objecte de la classe Process.
        /// </summary>
        /// <returns>El valor retornat de la comanda executada</returns>
        private string CheckDocker()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "docker";
                process.StartInfo.Arguments = "version";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                return output;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Funció que s'executa en el moment de fer clic al botó per 
        /// crear el contenidor de Docker
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void setDocker(object sender, RoutedEventArgs e)
        {
            if (this.CheckDocker().StartsWith("Client:"))
            {
                this.tabItemApi.IsEnabled = true;
            } 
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Funció qu es crida quan es fa clic al botó per Acceptar les IPs introduïdes,
        /// guardarles en el fitxer Config\\config_doc.json i cridar-les en el moment
        /// de fer les peticions
        /// </summary>
        private async void setApi(object sender, RoutedEventArgs e)
        {
            this.apiSql = this.menuApiSQL.txtIP.Text;
            this.apiMongoDB = this.menuApiMongoDB.txtIP.Text;
            this.apiHistorial = this.menuApiHistorial.txtIP.Text;

            if (this.apiSql != "" && this.apiMongoDB != ""
            && this.apiHistorial != "")
            {
                if (IPAddress.TryParse(this.apiSql, out ipSql) &&
                     IPAddress.TryParse(this.apiMongoDB, out ipMongoDB) &&
                     IPAddress.TryParse(this.apiHistorial, out ipHistorial))
                {
                    // Read configuration data from JSON file
                    this.jsonRuta = "Config\\config_doc.json";
                    string jsonContent = File.ReadAllText(jsonRuta);
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                    jsonObj["IPSQL"] = ipSql.ToString();
                    jsonObj["IPAudio"] = ipMongoDB.ToString();
                    jsonObj["IPHistorial"] = ipHistorial.ToString();
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonRuta, output);

                    jsonContent = File.ReadAllText(jsonRuta);
                    dynamic newjsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                    MessageBox.Show("Valor nou JSON: " + newjsonObj);

                    Application.Current.Dispatcher.Invoke(openMainWindow);
                    Application.Current.Dispatcher.Invoke(() => { Close(); });
                }
                else
                {
                    MessageBox.Show("IP/s en el format incorrecte");
                }
            }
            else
            {
                MessageBox.Show("Camp/s de text Buit/s");
            }
        }

        /// <summary>
        /// Funció per descarregar l'instalador de Docker un cop s'ha carregat
        /// la finestra ChooseDockerAndApi
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DownloadInstaller(object sender, RoutedEventArgs e)
        {
            this.jsonRuta = "Config\\config_doc.json";
            this.jsonContent = File.ReadAllText(this.jsonRuta);
            this.jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(this.jsonContent);
            this.urlDownloadDocker = jsonObj["urlDownloadDocker"];
            string installerFileName = "docker-installer.exe";

            using (WebClient wc = new WebClient())
            {
                try
                {
                    if (!File.Exists(installerFileName))
                    {
                        wc.DownloadFile(this.urlDownloadDocker, installerFileName);
                        MessageBox.Show("Fitxer .exe Descarregat");
                    }
                    ConfirmInstallDocker cid = new ConfirmInstallDocker();
                    cid.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descarregar instal·lador de Docker: " + ex.ToString());
                }
            }
        }
    }
}
