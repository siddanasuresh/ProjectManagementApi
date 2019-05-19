using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<int> InsertUser(User user);
        Task<int> EditUser(User user);
        Task<int> DeleteUser(User user);
    }
}
