using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apiMusicInfo.Models.Configurations
{
    public class BandMusicianConfigurations : IEntityTypeConfiguration<BandMusician>
    {
        public void Configure(EntityTypeBuilder<BandMusician> builder)
        {
            builder.HasKey(bm => new { bm.BandName, bm.BandFoundationDate, bm.MusicianName, bm.JoinDate });

            builder.HasOne(bm => bm.Band)
                .WithMany(b => b.BandMusicians)
                .HasForeignKey(bm => new { bm.BandName, bm.BandFoundationDate });

            builder.HasOne(bm => bm.Musician)
                .WithMany(m => m.BandMusicians)
                .HasForeignKey(bm => bm.MusicianName);
        }
    }
}
