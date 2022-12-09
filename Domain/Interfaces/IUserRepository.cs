using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetById(int id);
        Task<int> AddUser(User user);
        Task<int> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
