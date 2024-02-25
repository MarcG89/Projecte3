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
        private String jsonRuta = "Config\\config_doc.json";

        private IPAddress ipSql;
        private IPAddress ipMongoDB;
        private IPAddress ipHistorial;
        public ChooseDockerAndApi()
        {
            InitializeComponent(); 
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
                    string jsonContent = File.ReadAllText(jsonRuta);
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                    jsonObj["IPSQL"] = ipSql;
                    jsonObj["IPAudio"] = ipMongoDB;
                    jsonObj["IPHistorial"] = ipHistorial;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    MessageBox.Show("Valor nou JSON: " + output);
                    File.WriteAllText(jsonRuta, output);

                    MessageBox.Show("IPs correctes");
                }
                else
                {
                    MessageBox.Show("IPs en el format incorrecte");
                }
            }
            else
            {
                MessageBox.Show("Camps de text Buits");
            }
        }
    }
}
