using Microsoft.AspNetCore.Http;

namespace mla.ApiMusica.Model { 
    public class AudioUploadModel
    {
        public string Uid { get; set; }
        public IFormFile Audio { get; set; }
    }
}