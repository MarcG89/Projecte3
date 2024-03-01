using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using apiMusicInfo.Models;

namespace apiMusicInfo.Models
{
    public class Album
    {
        [Key]
        public string? Name { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public string FrontCover { get; set; }
        public string BackCover { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}