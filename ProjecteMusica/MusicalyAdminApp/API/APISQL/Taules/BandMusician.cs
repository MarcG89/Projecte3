using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class BandMusician
    {
        public string BandName { get; set; }
        public DateTime BandFoundationDate { get; set; }

        // Foreign key to Band
        public Band Band { get; set; }

        public string MusicianName { get; set; }
        public Musician Musician { get; set; }

        public DateTime JoinDate { get; set; }
    }
}
