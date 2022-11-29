using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Music_player.Data.Entity;
using Music_player.Data.Repository;
using System;
using System.Threading.Tasks;

namespace Music_player.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository _repository;
        private object login;

        public UserService(IGenericRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Add(string name, int id, int password)
        {
            {
                if (name.IsNullOrEmpty())
                    throw new ArgumentNullException();

                var newUser = await _repository.AddAsync(new User
                {
                    Name = name,
                    Login = (string)login,
                    Password = password,
                });

                return newUser.Id;
            }
        }

        public async Task UpdateUser(int userId, string name)
        {
            var user = await _repository.GetAll<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            user.Name = name;

            await _repository.UpdateAsync(user);
        }
    }
}