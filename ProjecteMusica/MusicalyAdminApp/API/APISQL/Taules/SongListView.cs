using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class SongListView
    {
        [Key]
        public Guid UID { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public int? Duration { get; set; }
    }
}
