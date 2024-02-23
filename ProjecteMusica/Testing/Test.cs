using MusicalyAdminApp.API.APISQL;
using MusicalyAdminApp.API.APISQL.Taules;
using MusicPlayerLibrary.Crypto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class Test
    {
        [SetUp]
        public void SetUp() { }

        [Test]
        public void EncryptPDFWithEncription()
        {
            string PDFruta = "CarpetaPDF\\Autovaloració.pdf";
            string PDFEncriptatRuta = "CarpetaPDF\\AutovaloracióEncrypted.pdf";
            string PDFDesencriptatRuta = "CarpetaPDF\\AutovaloracióDecrypted.pdf";
            string rutaCert = "CarpetaPDF\\certificat.pfx";
            string certPass = "123456";
            string publicKeyFile = "CarpetaPDF\\PublicKeyFile";

            RSACrypt.SavePublicKey(rutaCert, certPass, publicKeyFile);

            byte[] AESKey = Encryption.EncryptPDF(PDFruta, PDFEncriptatRuta, rutaCert, certPass, publicKeyFile);

            Encryption.DecryptPDF(PDFEncriptatRuta, PDFDesencriptatRuta, AESKey, rutaCert, certPass);

            byte[] rutaPDF = File.ReadAllBytes(PDFruta);
            byte[] rutaDecrypted = File.ReadAllBytes(PDFDesencriptatRuta);

            Assert.AreEqual(rutaDecrypted, rutaPDF);
        }

        [Test]
        public async Task CreateAndVerifyAPIAsync()
        {
            Apisql apisql = new Apisql();
            SongPostModel songPostModel = new SongPostModel
            {
                UID = Guid.NewGuid().ToString(),
                Title = "Hell is forever"
            };
            string response;
            response = await apisql.PostSong(songPostModel);
            Song songObject = JsonConvert.DeserializeObject<Song>(response);
            Assert.AreEqual(songPostModel.Title, songObject.Title);
        }
    }
}
