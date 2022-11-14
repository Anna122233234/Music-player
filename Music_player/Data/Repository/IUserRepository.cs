using Music_player.Data.Models;
using System.Threading.Tasks;

namespace Music_player.Data.Repository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);

    }
}
