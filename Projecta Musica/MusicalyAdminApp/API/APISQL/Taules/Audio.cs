using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalyAdminApp.API.APISQL.Taules
{
    public class Audio
    {
        public required string Name { get; set; }
        public required byte[] Content { get; set; }
    }
}
