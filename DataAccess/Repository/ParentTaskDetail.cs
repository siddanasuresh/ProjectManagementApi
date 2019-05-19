using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccess
{
    public class ParentTaskDetail : IParentTaskDetails
    {
        private readonly ProjectManagerApiDbContext _dbContext;
        public ParentTaskDetail()
        {
        }
        public ParentTaskDetail(ProjectManagerApiDbContext dbContext)
        {
            _dbContext = dbContext;          
        }       
        public Task<IEnumerable<ParentTaskDetails>> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public Task<ParentTaskDetails> Get(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<int> Insert(ParentTaskDetails parentTaskDetails)
        {
            throw new System.NotImplementedException();
        }
        public Task<int> Edit(ParentTaskDetails parentTaskDetails)
        {
            throw new System.NotImplementedException();
        }
        public Task<int> Delete(ParentTaskDetails parentTaskDetails)
        {
            throw new System.NotImplementedException();
        }
    }
}
