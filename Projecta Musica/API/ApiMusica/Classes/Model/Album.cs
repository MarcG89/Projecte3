using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ApiMusica.Classes.Model
{
    public class Album
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Titol { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
     
        public ObjectId FrontCover { get; set; }
        public ObjectId BackCover { get; set; }

    }
}
