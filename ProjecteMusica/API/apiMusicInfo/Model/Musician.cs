using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace apiMusicInfo.Models
{
    public class Musician
    {
        [Key]
        [MaxLength(15)]
        public string Name { get; set; }=null!;
        [Range(0, 99, ErrorMessage = "La edad debe estar entre 0 y 99.")]
        public int? Age { get; set; }
        public ICollection<BandMusician> BandMusicians { get; set; } = new List<BandMusician>();
        public ICollection<Play> Plays { get; set; } = new List<Play>();
    }
}