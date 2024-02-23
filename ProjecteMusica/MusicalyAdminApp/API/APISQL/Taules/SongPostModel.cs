using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class SongPostModel
    {
        [Key]
        public string UID { get; set; }
        public string Title { get; set; }
    }
}
