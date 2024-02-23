using System;

namespace apiMusicInfo.Models
{
    public class BandMusician
    {
        // Composite key consisting of BandName and BandFoundationDate
        public string BandName { get; set; }
        public DateTime BandFoundationDate { get; set; }

        // Foreign key to Band
        public Band Band { get; set; }

        public string MusicianName { get; set; }
        public Musician Musician { get; set; }

        public DateTime JoinDate { get; set; }
    }
}