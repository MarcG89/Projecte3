using System.Collections.Generic;
using ApiMusica.Classes.Model;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ApiMusica.Controllers.v1.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albums;

        public AlbumService(IMongoDatabase database)
        {
            _albums = database.GetCollection<Album>("albums");
        }

        public List<Album> Get() =>
            _albums.Find(album => true).ToList();

        public Album Get(string id) =>
            _albums.Find<Album>(album => album.Titol == id).FirstOrDefault();

        public Album Create(Album album)
        {
            _albums.InsertOne(album);
            return album;
        }
        public void Update(string id, Album albumIn) =>
            _albums.ReplaceOne(album => album.Titol == id, albumIn);
        public void Remove(Album albumIn) =>
            _albums.DeleteOne(album => album.Titol == albumIn.Titol);
        public void Remove(string id) =>
            _albums.DeleteOne(album => album.Titol == id);
    }
}
