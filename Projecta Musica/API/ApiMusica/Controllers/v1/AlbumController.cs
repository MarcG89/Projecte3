using ApiMusica.Classes.Model;
using ApiMusica.Controllers.v1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
            _albumService = albumService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Album>> Get() =>
            _albumService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAlbum")]
        public ActionResult<Album> Get(string id)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        [HttpPost]
        public ActionResult<Album> Create(Album album)
        {
            _albumService.Create(album);

            return CreatedAtRoute("GetAlbum", new { id = album.Titol.ToString() }, album);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Album albumIn)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumService.Update(id, albumIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var album = _albumService.Get(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumService.Remove(album.Titol);

            return NoContent();
        }
    }
}
