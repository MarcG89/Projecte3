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

        public async Task CreateAlbumWithImages(Album album)
        {
            // Guardar el álbum en la colección de álbumes
            await _albumCollection.InsertOneAsync(album);

            // Subir la portada a GridFS
            ObjectId frontCoverId = await SubirImagenAsync(album.FrontCover, "FrontCover", "image/jpeg");

            // Subir la contraportada a GridFS
            ObjectId backCoverId = await SubirImagenAsync(album.BackCover, "BackCover", "image/jpeg");

            // Actualizar los ObjectIds de las imágenes en el álbum

            // Actualizar el álbum en la colección
            var filter = Builders<Album>.Filter.Eq("_id", album.Titol);
            var update = Builders<Album>.Update
                .Set("FrontCoverId", frontCoverId)
                .Set("BackCoverId", backCoverId);
            await _albumCollection.UpdateOneAsync(filter, update);
        }

        public async Task<ObjectId> SubirImagenAsync(byte[] imagen, string nombreArchivo, string contentType)
        {
            using (var stream = new MemoryStream(imagen))
            {
                var options = new GridFSUploadOptions
                {
                    Metadata = new BsonDocument("contentType", contentType)
                };

                return await _gridFSBucket.UploadFromStreamAsync(nombreArchivo, stream, options);
            }
        }
    }
}
