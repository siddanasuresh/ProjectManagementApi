using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IParentTaskDetails
    {
        Task<IEnumerable<ParentTaskDetails>> GetAll();
        Task<ParentTaskDetails> Get(int id);
        Task<int> Insert(ParentTaskDetails parentTaskDetails);
        Task<int> Edit(ParentTaskDetails parentTaskDetails);
        Task<int> Delete(ParentTaskDetails parentTaskDetails);
    }
}
