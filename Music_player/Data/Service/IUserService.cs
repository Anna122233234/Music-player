using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_player.Services
{
    public interface IUserService
    {
        public  Task<int> Add(string name, int id, int password);
        public Task UpdateUser(int userId, string name);
    }
}