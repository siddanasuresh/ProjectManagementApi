using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccess
{
    public class UserRepository: IUser
    {
        ProjectManagerApiDbContext _dbContext;
        public UserRepository()
        {

        }
        public UserRepository(ProjectManagerApiDbContext dbContext)
        {
            _dbContext = dbContext;          
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }
        public async Task<User> GetUser(int userId)
        {
            return await _dbContext.Users.Include(user => user.Projects).Include(user => user.TaskDetails)
                .FirstOrDefaultAsync(task => task.UserId == userId);
        }
        public async Task<int> InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> EditUser(User user)
        {
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
