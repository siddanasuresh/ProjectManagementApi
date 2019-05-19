using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BusinessLogic
{
    public class ManageUser : IManageUser
    {
        IUser _user;

        public ManageUser()
        {
            
        }
        public ManageUser(IUser user)
        {
            _user = user;           
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _user.GetAllUsers();
        }
        public async Task<User> GetUser(int userId)
        {
            return await _user.GetUser(userId);
        }
        public async Task<int> InsertUser(User user)
        {
            return await _user.InsertUser(user);
        }
        public async Task<int> EditUser(User userDetail)
        {
            return await _user.EditUser(userDetail);
        }       
        public async Task<int> DeleteUser(User user)
        {
           return await _user.DeleteUser(user);
        }       
    }
}
