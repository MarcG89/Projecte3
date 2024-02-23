using MongoDB.Bson;

namespace ApiMusica.Classes.Model
{
    public class Album
    {
        public string Titol { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public IFormFile FrontCover { get; set; }
        public IFormFile BackCover { get; set; }

    }
}
