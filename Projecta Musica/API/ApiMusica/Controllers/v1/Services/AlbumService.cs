using System;
using System.IO;
using System.Threading.Tasks;
using ApiMusica.Classes.Model;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ApiMusica.Controllers.v1.Services
{
    public class AlbumService
    {
        private readonly IMongoCollection<Album> _albumCollection;
        private readonly GridFSBucket _gridFSBucket;

        public AlbumService(IMongoDatabase database)
        {
            _albumCollection = database.GetCollection<Album>("Album");
            _gridFSBucket = new GridFSBucket(database);
        }

        public async Task CreateAlbum(Album album)
        {
            await _albumCollection.InsertOneAsync(album);
        }

        public async Task<ObjectId> SubirImagenAsync(IFormFile imagen, string nombreArchivo, string contentType)
        {
            using (var stream = new MemoryStream())
            {
                await imagen.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var options = new GridFSUploadOptions
                {
                    Metadata = new BsonDocument("contentType", contentType)
                };

                return await _gridFSBucket.UploadFromStreamAsync(nombreArchivo, stream, options);
            }
        }

        public async Task<Album> GetByTitol(string titol)
        {
            return await _albumCollection.Find(album => album.Titol == titol).FirstOrDefaultAsync();
        }
        public async Task<List<Album>> GetAll()
        {
            return await _albumCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Stream> GetFrontCoverAsync(ObjectId frontCoverId)
        {
            return await _gridFSBucket.OpenDownloadStreamAsync(frontCoverId);
        }

    }
}
