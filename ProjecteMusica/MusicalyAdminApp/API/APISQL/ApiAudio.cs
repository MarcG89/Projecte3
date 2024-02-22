using MusicalyAdminApp.API.APISQL.Taules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL
{
    /// <summary>
    /// Class that provides methods for interacting with the Audio API.
    /// </summary>
    public class ApiAudio
    {
        private readonly HttpClient client;
        private String jsonRuta = "Config\\config_doc.json";
        private String url = "";

        /// <summary>
        /// Constructor for the ApiAudio class.
        /// Initializes the HttpClient instance with the base address and necessary headers.
        /// </summary>
        public ApiAudio()
        {
            // Read configuration data from JSON file
            string jsonContent = File.ReadAllText(jsonRuta);
            dynamic configData = JObject.Parse(jsonContent);
            url = configData.urlAudio;

            // Set up HttpClient with base address and headers
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Method to send audio data to the API for storage.
        /// </summary>
        /// <param name="uidSong">The unique identifier of the song associated with the audio.</param>
        /// <param name="musicPath">The path of the music file to be sent.</param>
        public async void PostAudio(string uidSong, string rutaMusica)
        {
            try
            {
                url += "api/Audio/GuardarWPF";

                byte[] contenidoMusica = File.ReadAllBytes(rutaMusica);

                Audio audio = new Audio
                {
                    Name = uidSong,  // Reemplaza con el nombre deseado
                    Content = contenidoMusica
                };

                HttpResponseMessage response = null;
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(audio), Encoding.UTF8, "application/json");
                    response = await client.PostAsJsonAsync(url, audio);
                }
                catch (HttpRequestException ex)
                {
                    Console.Out.WriteLine("Error en la petición HTTP ", ex);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error ", ex);
                }

                if (response != null && response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    string result = await response?.Content.ReadAsStringAsync();
                    Console.WriteLine(result ?? "Error en la petición HTTP");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSongs request: {ex.Message}");
            }
        }

        /// <summary>
        /// Disposes of the resources used by the HttpClient instance.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
