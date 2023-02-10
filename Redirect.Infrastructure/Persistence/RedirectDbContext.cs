using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Redirect.Core.Entities;

namespace Redirect.Infrastructure.Persistence
{
    public class RedirectDbContext : DbContext
    {
        public RedirectDbContext(DbContextOptions<RedirectDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}