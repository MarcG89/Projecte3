using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class Album    
    {
        [Key]
        public string? Titol { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string FrontCover { get; set; }
        public string BackCover { get; set; }
        public ICollection<Song>? Songs { get; set; }
    }
}
