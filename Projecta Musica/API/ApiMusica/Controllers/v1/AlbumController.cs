using ApiMusica.Classes.Model;
using ApiMusica.Controllers.v1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<ActionResult<Album>> CreateAlbumWithImages([FromBody] Album albumModel)
        {
            try
            {
                await _albumService.CreateAlbumWithImages(albumModel);
                return StatusCode(200, "El álbum se ha creado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el álbum.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
