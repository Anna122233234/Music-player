using Microsoft.EntityFrameworkCore;
using Music_player.Data.Models;

namespace Music_player.Data
{
    public class MusicDbContext : DbContext
    {
        
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
    }
}
