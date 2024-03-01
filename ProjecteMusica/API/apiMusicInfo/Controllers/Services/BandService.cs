using System;
using System.Collections.Generic;
using System.Linq;
using apiMusicInfo.Data;
using apiMusicInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace apiMusicInfo.Services
{
    public class BandService
    {
        private readonly DataContext _dbContext;

        public BandService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Band> GetAllBands()
        {
            return _dbContext.Bands.ToList();
        }

        public Band GetBand(string name, DateTime foundationDate)
        {
            return _dbContext.Bands.FirstOrDefault(b => b.Name == name && b.FoundationDate == foundationDate);
        }

        public Band CreateBand(Band band)
        {
            _dbContext.Bands.Add(band);
            _dbContext.SaveChanges();
            return band;
        }

        public Band UpdateBand(Band band)
        {
            _dbContext.Entry(band).State = EntityState.Modified;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BandExists(band.Name, band.FoundationDate))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return band;
        }

        public bool DeleteBand(string name, DateTime foundationDate)
        {
            var band = _dbContext.Bands.FirstOrDefault(b => b.Name == name && b.FoundationDate == foundationDate);
            if (band == null)
                return false;

            _dbContext.Bands.Remove(band);
            _dbContext.SaveChanges();
            return true;
        }

        private bool BandExists(string name, DateTime foundationDate)
        {
            return _dbContext.Bands.Any(b => b.Name == name && b.FoundationDate == foundationDate);
        }
    }
}
