using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ApiMusica.Classes.Model
{
    public class Album
    {
        public string Titol { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
     
        public byte[] FrontCover { get; set; }
        public byte[] BackCover { get; set; }

    }
}
