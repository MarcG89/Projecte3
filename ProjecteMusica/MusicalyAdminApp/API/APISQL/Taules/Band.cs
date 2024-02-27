using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class Band
    {
        [MaxLength(15)]
        public string Name { get; set; } = null!;
        public DateTime FoundationDate { get; set; }
        [MaxLength(15)]
        public string? Origin { get; set; }
        [MaxLength(15)]
        public string? Genre { get; set; }
        public ICollection<BandMusician> BandMusicians { get; set; } = new List<BandMusician>();
        public ICollection<Play> Plays { get; set; } = new List<Play>();
    }
}
