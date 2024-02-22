using MusicalyAdminApp.API.APISQL.Taules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL
{
    /// <summary>
    /// Class that provides methods for interacting with the Historic API.
    /// </summary>
    public class ApiHistoric : IDisposable
    {
        private readonly HttpClient client;
        private String jsonRuta = "Config\\config_doc.json";
        private String url = "";

        /// <summary>
        /// Constructor for the ApiHistoric class.
        /// Initializes the HttpClient instance with the base address and necessary headers.
        /// </summary>
        public ApiHistoric()
        {
            // Read configuration data from JSON file
            string jsonContent = File.ReadAllText(jsonRuta);
            dynamic configData = JObject.Parse(jsonContent);
            url = configData.urlHistorial;

            // Set up HttpClient with base address and headers
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Performs an asynchronous GET request and returns the response as a string.
        /// </summary>
        /// <param name="endpoint">The API endpoint for the GET request.</param>
        /// <returns>The response content as a string.</returns>
        public async Task<string> GetAsync(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in GET request: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// This method performs an HTTP PUT request, handle success or failure, and return the content of the response as a string.
        /// </summary>
        /// <param name="endpoint">The API endpoint for the PUT request.</param>
        /// <param name="jsonContent">The JSON content to be sent in the request.</param>
        /// <returns>The response content as a string.</returns>
        public async Task<string> PutAsync(string endpoint, string jsonContent)
        {
            try
            {
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in PUT request: {ex.Message}");
                throw; // You can throw a custom exception if you prefer
            }
        }

        /// <summary>
        /// Retrieves the historical data asynchronously from the API.
        /// </summary>
        /// <returns>A list of historical data.</returns
        public async Task<List<Historial>> GetHistorial()
        {
            try
            {
                string endpoint = "api/Historial";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Historial>();
                }

                var historial = JsonConvert.DeserializeObject<List<Historial>>(responseJson);
                return historial;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSongs request: {ex.Message}");
                return new List<Historial>();
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
