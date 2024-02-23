using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiMusica.Classes.Model;
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

            // Configurar GridFSBucket
            _gridFSBucket = new GridFSBucket(database);
        }

        // Método para crear un nuevo álbum
        public async Task CreateAlbum(Album album)
        {
            await _albumCollection.InsertOneAsync(album);
        }
        // Método para subir imágenes a GridFS
        public async Task<ObjectId> SubirImagenAsync(IFormFile imagen, string nombreArchivo)
        {
            using (var stream = new MemoryStream())
            {
                await imagen.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin); // Reiniciar la posición del flujo

                // Opciones para la carga de GridFS
                var options = new GridFSUploadOptions
                {
                    Metadata = new BsonDocument("contentType", imagen.ContentType) // Opcional: Puedes agregar metadatos adicionales
                };

                return await _gridFSBucket.UploadFromStreamAsync(nombreArchivo, stream, options);
            }
        }
    }
}
