using Music_player.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music_player.Data.Repository
{
    public interface IGenericRepository
    {
        public Task AddUserAsync(User user);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<User> GetUserByIdAsync(int userId);
        public Task<List<User>> GetAllUserAsync();
        public Task UpdateUserAsync(User newData);
        public Task DeleteUserAsync(int userId);
    }
}
