using Microsoft.EntityFrameworkCore;
using Music_player.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Music_player.Data.Models;
using System;

namespace Music_player.Data.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly MusicDbContext _musicdbcontext;
        public GenericRepository(MusicDbContext context)
        {
            _musicdbcontext = context;
        }

        public async Task AddUserAsync(User user)
        {
            if (user.Name.IsNullOrEmpty())
            {
                throw new ArgumentException("No correctly Data");
            }

            await _musicdbcontext.Users.AddAsync(user);
            await _musicdbcontext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {

            var user = await _musicdbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ArgumentNullException("This user is not exist");
            }
            _musicdbcontext.Users.Remove(user);
            await _musicdbcontext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var users = await _musicdbcontext.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _musicdbcontext.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (username is null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            return await _musicdbcontext.Users.SingleOrDefaultAsync(u => u.Name == username);
        }

        public async Task UpdateUserAsync(User newData)
        {
            if (newData == null)
            {
                throw new ArgumentNullException("New data is null");
            }

            var user = await _musicdbcontext.Users.SingleOrDefaultAsync(u => u.Id == newData.Id);

            if (user == null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            if (user.Name != newData.Name)
            {
                user.Name = newData.Name;
            }

            if (user.Id != newData.Id)
            {
                user.Id = newData.Id;
            }

            await _musicdbcontext.SaveChangesAsync();
        }
    }
}

