using Music_player.Data.Models;
using System.Threading.Tasks;

namespace Music_player.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicDbContext _musicDbContext;

        public UserRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _musicDbContext.Users.AddAsync(user);
            await _musicDbContext.SaveChangesAsync();
        }
    }
}
