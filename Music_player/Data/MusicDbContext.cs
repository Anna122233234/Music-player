using Microsoft.EntityFrameworkCore;
using Music_player.Data.Configurations;

namespace Music_player.Data
{
    public class MusicDbContext : DbContext
    {

        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
    }

}

