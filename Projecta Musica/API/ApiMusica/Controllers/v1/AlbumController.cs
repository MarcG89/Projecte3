using ApiMusica.Classes.Model;
using ApiMusica.Controllers.v1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMusica.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(AlbumService albumService, ILogger<AlbumController> logger)
        {
            _albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<Album>> CreateAlbum([FromForm] Album albumModel)
        {
            try
            {
                // Subir las imágenes a GridFS y obtener los ObjectIds
               await _albumService.SubirImagenAsync(albumModel.FrontCover, $"{albumModel.Titol}_FrontCover.jpg");
               await _albumService.SubirImagenAsync(albumModel.BackCover, $"{albumModel.Titol}_BackCover.jpg");

                // Crear el objeto Album con los datos del álbum y los ObjectIds de las imágenes
                var album = new Album
                {
                    Titol = albumModel.Titol,
                    Year = albumModel.Year,
                    Gender = albumModel.Gender,
                };
                   
                // Crear el álbum en la base de datos
                await _albumService.CreateAlbum(album);

                return StatusCode(200, "Creat");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el álbum.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}
