using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apiMusicInfo.Models
{
    public class Play
    {
        public string? Bandname { get; set; }
        public DateTime BandDateFoundation { get; set; }
        public Band? Band { get; set; }

        public string? MusicianName { get; set; }
        public Musician? Musician { get; set; }

        public string? InstrumentName { get; set; }
        public Instrument? Instrument { get; set; }

        public Guid SongUID { get; set; }
        public Song? Song { get; set; }
    }
}