using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
     public class SongOriginal
    {
        public int Id { get; set; }
        public List<Song> values { get; set; } = new List<Song>();
    }
}
