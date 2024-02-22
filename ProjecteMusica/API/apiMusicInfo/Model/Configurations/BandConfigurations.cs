using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using apiMusicInfo.Models;ç

namespace apiMusicInfo.Models.Configurations
{
    public class BandConfigurations : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.HasKey(key => new { key.Name, key.FoundationDate });
        }
    }
}