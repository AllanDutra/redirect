using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redirect.Core.Entities;

namespace Redirect.Infrastructure.Configurations
{
    public class ShortenedUrlConfigurations : IEntityTypeConfiguration<ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.HasKey(e => e.Code).HasName("shortenedURLs_pkey");

            builder.ToTable("shortenedURLs");

            builder.Property(e => e.Code).HasMaxLength(10);
            builder.Property(e => e.Expiration).HasColumnType("timestamp without time zone");
            builder.Property(e => e.OriginalUrl)
                .IsRequired()
                .HasMaxLength(2048)
                .HasColumnName("OriginalURL");
        }
    }
}