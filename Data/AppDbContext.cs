using JoyBoxPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace JoyBoxPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games => Set<Game>();
    }
}
