using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMusicInfo.Data;
using apiMusicInfo.Models;

namespace apiMusicInfo.Services
{
    public class MusicianService
    {
        private readonly DataContext _context;

        public MusicianService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Musician>> GetMusiciansAsync()
        {
            return await _context.Musicians.ToListAsync();
        }

        public async Task<Musician> GetMusicianAsync(string id)
        {
            return await _context.Musicians.FindAsync(id);
        }

        public async Task AddMusicianAsync(Musician musician)
        {
            _context.Musicians.Add(musician);
            await _context.SaveChangesAsync();
        }
    }
}