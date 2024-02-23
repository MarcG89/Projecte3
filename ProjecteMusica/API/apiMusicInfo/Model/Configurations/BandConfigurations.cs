using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apiMusicInfo.Models.Configurations
{
    public class BandConfigurations : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.HasKey(b => new { b.Name, b.FoundationDate });

        }
    }
}
