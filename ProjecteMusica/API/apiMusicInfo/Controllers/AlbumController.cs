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
        public async Task<ActionResult<Album>> GetAlbum(string title, int year)
        {
            var album = await _albumService.GetAlbum(title, year);

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
            return CreatedAtAction(nameof(GetAlbum), new { title = album.Name }, album);
        }

        [HttpPut("{title}")]
        public async Task<IActionResult> PutAlbum(string title, Album album)
        {
            if (title != album.Name)
            {
                return BadRequest();
            }

            await _albumService.UpdateAlbum(album);

            return NoContent();
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteAlbum(string title, int year)
        {
            var albumToDelete = await _albumService.GetAlbum(title, year);
            if (albumToDelete == null)
            {
                return NotFound();
            }

            await _albumService.DeleteAlbum(albumToDelete.Name);
            return NoContent();
        }

        // Nueva acción para obtener canciones relacionadas con un álbum
        [HttpGet("{title}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByAlbum(string title)
        {
            var songs = await _albumService.GetSongsByAlbum(title);
            return Ok(songs);
        }
    }
}
