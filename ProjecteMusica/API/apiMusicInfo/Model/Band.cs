using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace apiMusicInfo.Models
{
    public class Band
    {
        [MaxLength(15)]
        public string Name { get; set; }=null!;
        public DateTime FoundationDate { get; set; }
        [MaxLength(15)]
        public string? Origin { get; set; }
        [MaxLength(15)]
        public string? Genre { get; set; }
        public ICollection<BandMusician> BandMusicians { get; set; } = new List<BandMusician>();
        public ICollection<Play> Plays { get; set; } = new List<Play>();
    }
}