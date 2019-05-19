using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IManageUser
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<int> InsertUser(User user);
        Task<int> EditUser(User user);
        Task<int> DeleteUser(User user);      
    }
}
