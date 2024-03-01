using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiMusicInfo.Models;
using apiMusicInfo.Services;

namespace apiMusicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            var albums = await _albumService.GetAlbums();
            return Ok(albums);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Album>> GetAlbum(string title)
        {
            var album = await _albumService.GetAlbum(title);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            await _albumService.CreateAlbum(album);
            return CreatedAtAction(nameof(GetAlbum), new { title = album.Titol }, album);
        }

        [HttpPut("{title}")]
        public async Task<IActionResult> PutAlbum(string title, Album album)
        {
            if (title != album.Titol)
            {
                return BadRequest();
            }

            await _albumService.UpdateAlbum(album);

            return NoContent();
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteAlbum(string title)
        {
            var albumToDelete = await _albumService.GetAlbum(title);
            if (albumToDelete == null)
            {
                return NotFound();
            }

            await _albumService.DeleteAlbum(albumToDelete.Titol);
            return NoContent();
        }
    }
}
