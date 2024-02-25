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

        private Regex regexIP = new Regex(@"/^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9])$/");
        //private Regex regexIP = new Regex(@"/^((?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])[.]){3}(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])$/");
        
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
                if (this.apiSql != "0.0.0.0" && this.apiMongoDB != "0.0.0.0"
                && this.apiHistorial != "0.0.0.0" && !this.apiSql.Contains("255")
                && !this.apiMongoDB.Contains("255") && !this.apiHistorial.Contains("255")
                && !this.apiSql.EndsWith(".0") && !this.apiMongoDB.EndsWith(".0")
                && !this.apiHistorial.EndsWith(".0") && !this.regexIP.IsMatch(this.apiSql)
                && !this.regexIP.IsMatch(this.apiMongoDB) && !this.regexIP.IsMatch(this.apiHistorial))
                {
                    // Read configuration data from JSON file
                    string jsonContent = File.ReadAllText(jsonRuta);
                    /*MessageBox.Show(jsonContent);
                    dynamic configData = JObject.Parse(jsonContent);
                    configData.IPSQL = apiSql;
                    configData.IPHistorial = apiHistorial;
                    configData.IPAudio = apiMongoDB;

                    string nouContingutJson = configData.ToString();
                    File.WriteAllText(jsonRuta, nouContingutJson);*/



                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
                    jsonObj["IPSQL"] = apiSql;
                    jsonObj["IPAudio"] = apiMongoDB;
                    jsonObj["IPHistorial"] = apiHistorial;
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
