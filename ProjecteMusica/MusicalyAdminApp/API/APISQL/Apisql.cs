using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MusicalyAdminApp.API.APISQL.Taules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicalyAdminApp.API.APISQL
{
    /// <summary>
    /// Represents a class for interacting with the SQL API for managing musical data.
    /// </summary>
    public class Apisql : IDisposable
    {
        private readonly HttpClient client;
        private String jsonRuta = "Config\\config_doc.json";
        private String url = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Apisql"/> class and establishes a connection to the API with access to the database.
        /// </summary>
        public Apisql()
        {
            // Read configuration data from JSON file
            string jsonContent = File.ReadAllText(jsonRuta);
            dynamic configData = JObject.Parse(jsonContent);
            url = configData.urlSQL;

            // Set up HttpClient with base address and headers
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region HTTP Methods

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
        /// This method performs an HTTP POST request, handles success or failure, and returns the content of the response as a string.
        /// </summary>
        /// <param name="endpoint">The API endpoint for the POST request.</param>
        /// <param name="jsonContent">The JSON content to be sent in the request.</param>
        /// <returns>The response content as a string.</returns>
        public async Task<string> PostAsync(string endpoint, string jsonContent)
        {
            try
            {
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in POST request: {ex.Message}");
                throw; // You can throw a custom exception if you prefer
            }
        }
        #endregion

        #region Song Methods

        /// <summary>
        /// Retrieves information for all songs from the API.
        /// </summary>
        /// <returns>List of songs.</returns>
        public async Task<List<Song>> GetSongs()
        {
            try
            {
                string endpoint = "api/Song";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Song>();
                }

                var songs = JsonConvert.DeserializeObject<List<Song>>(responseJson);
                return songs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSongs request: {ex.Message}");
                return new List<Song>();
            }
        }

        /// <summary>
        /// Retrieves information for a specific song based on its unique identifier from the API.
        /// </summary>
        /// <param name="songuid">Unique identifier of the song.</param>
        /// <returns>List containing the specific song.</returns>
        public async Task<List<Song>> GetSong(string songuid)
        {
            try
            {
                string endpoint = $"api/Song/{songuid}";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Song>();
                }

                var songs = JsonConvert.DeserializeObject<List<Song>>(responseJson);
                return songs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSong request: {ex.Message}");
                return new List<Song>();
            }
        }

        /// <summary>
        /// Updates information for a specific song using its unique identifier.
        /// </summary>
        /// <param name="songuid">Unique identifier of the song.</param>
        /// <param name="updatedSong">Updated information for the song.</param>
        /// <returns>Response from the API.</returns>
        public async Task<string> UpdateSong(String songuid, Song updatedSong)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(updatedSong);

                // Construct the API endpoint for updating the song
                string endpoint = $"api/Song/{songuid}";

                // Perform the PUT request
                string responseJson = await PutAsync(endpoint, jsonContent);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during UpdateSong request: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// This method performs an HTTP POST request to create a new song, handles success or failure, and returns the content of the response as a string.
        /// </summary>
        /// <param name="songPostModel">The song data to be posted.</param>
        /// <returns>The response content as a string.</returns>
        public async Task<string> PostSong(SongPostModel songPostModel)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(songPostModel);

                // Construct the API endpoint for creating a new song
                string endpoint = "api/Song";

                // Perform the POST request
                string responseJson = await PostAsync(endpoint, jsonContent);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PostSong request: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Adds an extension to a specific song using its unique identifier.
        /// </summary>
        /// <param name="songId">Unique identifier of the song.</param>
        /// <param name="extension">Extension to be added to the song.</param>
        /// <returns>Response from the API.</returns>
        public async Task<string> AddExtension(String songId, string extension)
        {
            try
            {
                string endpoint = $"api/Song/AddExtension/{songId}/{extension}";
                string responseJson = await PutAsync(endpoint, string.Empty);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during AddExtension request: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Adds duration information to a specific song using its unique identifier.
        /// </summary>
        /// <param name="songId">Unique identifier of the song.</param>
        /// <param name="duration">Duration information to be added to the song.</param>
        /// <returns>Response from the API.</returns>
        public async Task<string> AddDuration(string songId, string duration)
        {
            try
            {
                string endpoint = $"api/Song/AddDuration/{songId}/{duration}";
                string responseJson = await PutAsync(endpoint, string.Empty);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during AddDuration request: {ex.Message}");
                return string.Empty;
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Retrieves information for all albums from the API.
        /// </summary>
        /// <returns>List of albums.</returns>
        public async Task<List<Album>> GetAlbums()
        {
            try
            {
                string endpoint = "api/Album";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Album>();
                }

                var albums = JsonConvert.DeserializeObject<List<Album>>(responseJson);
                return albums;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAlbums request: {ex.Message}");
                return new List<Album>();
            }
        }

        /// <summary>
        /// Retrieves information for all SongsOriginals by Album Name from the API.
        /// </summary>
        /// <param name="name">The Album's name from which we want to get its Songs.</param>
        /// <returns>List of SongsOriginals.</returns>
        public async Task<List<Song>> GetSongsAlbumByName(string name)
        {
            try
            {
                string endpoint = "http://172.23.2.141:5095/api/Album/" + name + "/songs";
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseJson = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(responseJson))
                {
                    var songs = JsonConvert.DeserializeObject<List<Song>>(responseJson);
                    return songs;
                }

                return new List<Song>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSongsAlbumByName request: {ex.Message}");
                return new List<Song>();
            }
        }


        /// <summary>
        /// Asynchronously updates an album in the API based on the provided album ID and updated album data.
        /// </summary>
        /// <param name="id">The unique identifier of the album to be updated.</param>
        /// <param name="updateAlbum">The updated Album object containing the modified data.</param>
        /// <returns>A task representing the asynchronous operation, returning a string response from the API.</returns>
        public async Task<string> UpdateAlbum(string id, Album updateAlbum)
        {
            try
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(updateAlbum);

                    // Construct the API endpoint for updating the song
                    string endpoint = $"api/Album/{id}";

                    // Perform the PUT request
                    string responseJson = await PutAsync(endpoint, jsonContent);

                    return responseJson;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during UpdateSong request: {ex.Message}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PostAlbum request: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of bands from the API.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, returning a List of Band objects retrieved from the API.
        /// If an error occurs during the operation, an empty List of Band objects is returned.
        /// </returns>
        public async Task<List<Band>> GetBands()
        {
            try
            {
                string endpoint = "api/Band";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Band>();
                }

                var bands = JsonConvert.DeserializeObject<List<Band>>(responseJson);
                return bands;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAlbums request: {ex.Message}");
                return new List<Band>();
            }
        }

        /// <summary>
        /// Asynchronously updates a band's information on the API.
        /// </summary>
        /// <param name="id">The unique identifier of the band to be updated.</param>
        /// <param name="updateBand">The Band object containing the updated information.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a string representing the response
        /// from the API after attempting to update the band. If the operation is successful, the response
        /// contains information about the updated band. If an error occurs during the operation, an
        /// empty string is returned.
        /// </returns>
        public async Task<string> UpdateBand(string id, Band updateBand)
        {
            try
            {
                try
                {
                    string jsonContent = JsonConvert.SerializeObject(updateBand);

                    // Construct the API endpoint for updating the song
                    string endpoint = $"api/Band/{id}";

                    // Perform the PUT request
                    string responseJson = await PutAsync(endpoint, jsonContent);

                    return responseJson;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during UpdateSong request: {ex.Message}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PostAlbum request: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of musicians from the API.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, returning a List of Musician objects
        /// obtained from the API. If the operation is successful, the list contains information about
        /// the available musicians. If an error occurs during the operation, an empty list is returned.
        /// </returns>
        public async Task<List<Musician>> GetMusicians()
        {
            try
            {
                string endpoint = "api/Musician";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Musician>();
                }

                var musicians = JsonConvert.DeserializeObject<List<Musician>>(responseJson);
                return musicians;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetSongs request: {ex.Message}");
                return new List<Musician>();
            }
        }

        /// <summary>
        /// Asynchronously updates musician information on the API using a PUT request.
        /// </summary>
        /// <param name="id">The unique identifier of the musician to be updated.</param>
        /// <param name="updatedMusician">The Musician object containing the updated information.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a string containing
        /// the response JSON from the API after attempting to update the musician.
        /// If the operation is successful, the JSON response contains details about the updated musician.
        /// If an error occurs during the operation, an empty string is returned.
        /// </returns>
        public async Task<string> UpdateMusician(String id, Musician updatedMusician)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(updatedMusician);

                // Construct the API endpoint for updating the song
                string endpoint = $"api/Musician/{id}";

                // Perform the PUT request
                string responseJson = await PutAsync(endpoint, jsonContent);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during UpdateSong request: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of instruments from the API using a GET request.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, returning a list of Instrument objects.
        /// If the operation is successful, the list contains details about the instruments retrieved from the API.
        /// If an error occurs during the operation, an empty list of instruments is returned.
        /// </returns>
        public async Task<List<Instrument>> GetInstruments()
        {
            try
            {
                string endpoint = "api/Instrument";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Instrument>();
                }

                var instruments = JsonConvert.DeserializeObject<List<Instrument>>(responseJson);
                return instruments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetAlbums request: {ex.Message}");
                return new List<Instrument>();
            }
        }

        /// <summary>
        /// Asynchronously updates an instrument's information on the API using a PUT request.
        /// </summary>
        /// <param name="id">The unique identifier of the instrument to be updated.</param>
        /// <param name="updatedInstrument">The Instrument object containing the updated information.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a string indicating the result of the update.
        /// If the operation is successful, the string contains details about the update.
        /// If an error occurs during the operation, an empty string is returned.
        /// </returns>
        public async Task<string> UpdateInstrument(String id, Instrument updatedInstrument)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(updatedInstrument);

                // Construct the API endpoint for updating the song
                string endpoint = $"api/Instrument/{id}";

                // Perform the PUT request
                string responseJson = await PutAsync(endpoint, jsonContent);

                return responseJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during UpdateSong request: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Retrieves information for all extensions from the API.
        /// </summary>
        /// <returns>List of extensions.</returns>
        public async Task<List<Extension>> GetExtensions()
        {
            try
            {
                string endpoint = "api/Extension";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<Extension>();
                }

                var extensions = JsonConvert.DeserializeObject<List<Extension>>(responseJson);
                return extensions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetExtensions request: {ex.Message}");
                return new List<Extension>();
            }
        }

        /// <summary>
        /// Retrieves information for all playlists from the API.
        /// </summary>
        /// <returns>List of playlists.</returns>
        public async Task<List<PlayList>> GetPlaylists()
        {
            try
            {
                string endpoint = "api/Playlist";
                string responseJson = await GetAsync(endpoint);

                if (string.IsNullOrEmpty(responseJson))
                {
                    return new List<PlayList>();
                }

                var playlists = JsonConvert.DeserializeObject<List<PlayList>>(responseJson);
                return playlists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during GetPlaylists request: {ex.Message}");
                return new List<PlayList>();
            }
        }

        #endregion

        /// <summary>
        /// Releases the resources used by the HttpClient.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
