using iText.Signatures;
using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.Taules;
using MusicPlayerLibrary.Certificates;
using MusicPlayerLibrary.Crypto;
using MusicPlayerLibrary.GestioPDF;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

class ServerProgram
{
    static int PORT = 999;
    static TcpListener listener;
    static TcpClient ActualClient;
    static String PublicKeyString;
    static String ListaPedida;
    static String PDFEncriptado = "ServerFitxers\\PDFCrypted.pdf";
    static String PDFSignat = "ServerFitxers\\PDFsignat.pdf";
    static String RutaPublicKey = "ServerFitxers\\PublicKeyFile.pem";
    static String RutaCertificado = "ServerFitxers\\certificat.pfx";
    static String CertPass = "123456";
    static String jsonRuta = "Config\\config_doc.json";
    static String IPServer="";

    /// <summary>
    /// Main funtion of server
    /// </summary>
    public static void Main()
    {
        string jsonContent = File.ReadAllText(jsonRuta);
        dynamic configData = JObject.Parse(jsonContent);
        IPServer = configData.IP;

        listener = new TcpListener(IPAddress.Parse(IPServer), PORT);
        listener.Start();
        Console.WriteLine("Socket Inciado y escuchando...");

        try
        {
            ActualClient = listener.AcceptTcpClient();
            Console.WriteLine("Conexió correcta amb el client");

            // Check if the certificate file already exists
            if (!File.Exists(RutaCertificado))
            {
                // If not, generate the certificate
                Autosigned.GeneratePfx(RutaCertificado, CertPass);
            }


            Listener();

            // Generar datos para el PDF
            GenerarPDFAsync().GetAwaiter().GetResult();

            // Encriptar PDF
            var AESKey = Encryption.EncryptPDF(PDFSignat, PDFEncriptado, RutaCertificado, CertPass, RutaPublicKey);

            // Retornar PDF amb consulta i encriptada amb clau pública
            Sender(PDFEncriptado, AESKey);


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            // Seveix per tancar el programa
            listener.Stop();
        }
    }

    /// <summary>
    /// Asynchronous method to generate a PDF based on the requested list option.
    /// </summary>
    private static async Task GenerarPDFAsync()
    {
        try
        {
            // Asynchronously get the list of songs
            Apisql api = new Apisql();
            string jsonString = "";

            // Determine the type of list requested
            if (ListaPedida.Equals("1"))
            {
                // Get the list of songs
                List<Song> songs = await api.GetSongs();
                var data = new
                {
                    Cancions = songs
                };

                // Serialize the object to a JSON string
                jsonString = JsonSerializer.Serialize(data);
            }
            else if (ListaPedida.Equals("2"))
            {
                // Get the list of extensions
                List<Extension> extensions = await api.GetExtensions();
                var data = new
                {
                    Extensions = extensions
                };

                // Serialize the object to a JSON string
                jsonString = JsonSerializer.Serialize(data);
            }
            else if (ListaPedida.Equals("3"))
            {
                // Get the list of playlists
                List<PlayList> playlists = await api.GetPlaylists();
                var data = new
                {
                    Playlists = playlists
                };

                // Serialize the object to a JSON string
                jsonString = JsonSerializer.Serialize(data);
            }
            else if (ListaPedida.Equals("4"))
            {
                // Get the list of instruments
                List<Instrument> instruments = await api.GetInstruments();
                var data = new
                {
                    Instruments = instruments
                };

                // Serialize the object to a JSON string
                jsonString = JsonSerializer.Serialize(data);
            }
            else
            {
                // Invalid option
                var data = new
                {
                    Error = "Invalid option!"
                };
            }

            // Create PDF
            CreatePDF.CrearPDFSignat(PDFSignat, jsonString, CertPass, RutaCertificado);
        }
        catch (Exception ex)
        {
            // Handle exceptions when generating the PDF
            Console.WriteLine($"Error generating the PDF: {ex.Message}");
        }
    }

    /// <summary>
    /// Sends an encrypted file to the client through the network connection.
    /// </summary>
    /// <param name="filePath">Path of the file to send.</param>
    /// <param name="aesKey">AES key used for file encryption.</param>
    private static void Sender(string filePath, byte[] aesKey)
    {
        try
        {
            // Get the network stream reference of the client
            NetworkStream networkStream = ActualClient.GetStream();

            // Send the AES key to the client
            networkStream.Write(aesKey, 0, aesKey.Length);

            // Read the file in blocks and send it to the client
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead;

                // Send the file to the client in blocks
                while ((bytesRead = fileStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    networkStream.Write(buffer, 0, bytesRead);
                }
            }

            Console.WriteLine($"Encrypted file {filePath} sent successfully.");

            // Close the connection
            networkStream.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing client: {ex.Message}");
        }
    }

    /// <summary>
    /// Listens for incoming messages from the client through the network stream.
    /// </summary>
    private static void Listener()
    {
        try
        {
            // Get the network stream reference of the client
            NetworkStream networkStream = ActualClient.GetStream();

            // Initialize the buffer size
            const int bufferSize = 1_024;
            byte[] buffer = new byte[bufferSize];

            // Read data from the network stream
            int bytesRead = networkStream.Read(buffer, 0, bufferSize);

            if (bytesRead > 0)
            {
                // Convert received bytes to string
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Split the message into parts
                string[] messageSplited = message.Split("|");
                ListaPedida = messageSplited[0];
                PublicKeyString = messageSplited[1];

                // Save the received public key to a file
                File.WriteAllBytes(RutaPublicKey, Convert.FromBase64String(PublicKeyString));

                Console.Write("Received Key! The public key is: " + PublicKeyString);
                Console.Write("The requested list is: " + ListaPedida);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in the server listener: {ex.Message}");
        }
    }
}