using MusicPlayerLibrary.Certificates;
using MusicPlayerLibrary.Crypto;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
    /// <summary>
    /// Represents the client-side program for interacting with a server over a TCP connection.
    /// </summary>
    public class Program
    {
        static int PORT = 999;
        static TcpClient ActualClient;
        static String PDFReceived = "ClientFitxers\\ReceivedPDF.pdf";
        static String DecryptedPDF = "ClientFitxers\\ReceivedAndDecryptedPDF.pdf";
        static String PublicKeyPath = "ClientFitxers\\PublicFileKey";
        static String CertificatePath = "ClientFitxers\\certificate.pfx";
        static String CertPass = "123456";
        static String jsonPath = "Config\\config_doc.json";
        static String ServerIP = "";

        /// <summary>
        /// Main entry point of the client program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                // Read server IP from configuration file
                string jsonContent = File.ReadAllText(jsonPath);
                dynamic configData = JObject.Parse(jsonContent);
                ServerIP = configData.IP;

                // Connect to the server
                var ipEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), PORT);
                ActualClient = new TcpClient();
                ActualClient.Connect(ipEndPoint);
                Console.WriteLine("Connection established with the server");

                // Prompt user for a list to query
                Console.WriteLine("Enter a list to make the query: ");
                string response = Console.ReadLine();

                // Check if the certificate file already exists
                if (!File.Exists(CertificatePath))
                {
                    // If not, generate the certificate
                    Autosigned.GeneratePfx(CertificatePath, CertPass);
                }

                // Save and load RSA public key
                RSACrypt.SavePublicKey(CertificatePath, CertPass, PublicKeyPath);
                RSA rsa = RSACrypt.LoadPublicKey(PublicKeyPath);

                byte[] keyBytes = File.ReadAllBytes(PublicKeyPath);
                var keyBytesString = Convert.ToBase64String(keyBytes);
                // Export public key to a string
                byte[] publicKeyBytes = rsa.ExportRSAPublicKey();
                String publicKeyString = Convert.ToBase64String(publicKeyBytes);

                // Now you can use publicKeyString as a string in your response
                response = response + "|" + keyBytesString;
                SenderMessage(response);

                NetworkStream networkStream = ActualClient.GetStream();
                byte[] encryptedAesKey = ReceiveMessage(networkStream);
                Receiver(PDFReceived, networkStream, encryptedAesKey);
            }
            finally
            {
                if (ActualClient != null)
                {
                    ActualClient.Close();
                }
            }
        }

        /// <summary>
        /// Receives a message from the network stream.
        /// </summary>
        /// <param name="networkStream">The network stream from which to receive the message.</param>
        /// <returns>The received message as a byte array.</returns>
        static public byte[] ReceiveMessage(NetworkStream networkStream)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[256];

                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);

                return buffer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while receiving data: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Receives a file from the network stream and decrypts it.
        /// </summary>
        /// <param name="destinationPath">The path where the received and decrypted file will be saved.</param>
        /// <param name="networkStream">The network stream from which to receive the file.</param>
        /// <param name="encryptedAesKey">The encrypted AES key used for decryption.</param>
        static public void Receiver(string destinationPath, NetworkStream networkStream, byte[] encryptedAesKey)
        {
            try
            {
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];

                using (FileStream fileStream = File.Create(destinationPath))
                {
                    int bytesRead;

                    // Receive the file in blocks
                    while ((bytesRead = networkStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                // Decrypt the file
                Encryption.DecryptPDF(PDFReceived, DecryptedPDF, encryptedAesKey, CertificatePath, CertPass);
                // Close the connection
                networkStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing the server's response: {ex.Message}");
            }
        }

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        /// <param name="message">The message to be sent to the server.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        static async Task SenderMessage(string message)
        {
            try
            {
                // Get the network stream reference of the client
                NetworkStream stream = ActualClient.GetStream();
                var EncMessage = Encoding.UTF8.GetBytes(message);
                StreamWriter writer = new StreamWriter(stream);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
                Console.WriteLine("Sent: " + message);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
        }
    }
}
