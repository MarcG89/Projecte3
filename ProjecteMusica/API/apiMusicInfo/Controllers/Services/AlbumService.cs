using Microsoft.EntityFrameworkCore;
using apiMusicInfo.Data;
using apiMusicInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiMusicInfo.Services
{
    public class AlbumService
    {
        private readonly DataContext _context;

        public AlbumService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Album>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<Album> GetAlbum(string title, int year)
        {
            return await _context.Albums.FindAsync(title, year);
        }

        public async Task CreateAlbum(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAlbum(Album album)
        {
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlbum(string title)
        {
            var albumToDelete = await _context.Albums.FindAsync(title);
            _context.Albums.Remove(albumToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Song>> GetSongsByAlbum(string albumTitle)
        {
            return await _context.Songs
                .Include(s => s.Albums) // Incluir la relación con los álbumes
                .Where(s => s.Albums.Any(a => a.Name == albumTitle)) // Filtrar las canciones que pertenecen al álbum especificado
                .ToListAsync();
        }


    }
}
