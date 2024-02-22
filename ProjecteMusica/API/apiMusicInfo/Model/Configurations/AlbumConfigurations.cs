using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using apiMusicInfo.Models;


namespace apiMusicInfo.Models.Configurations
{
    public class AlbumConfigurations : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(key => new { key.Titol, key.Year });
        }
    }
}