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
        public string Name { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
     
        public string FrontCover { get; set; }
        public string BackCover { get; set; }

    }
}
