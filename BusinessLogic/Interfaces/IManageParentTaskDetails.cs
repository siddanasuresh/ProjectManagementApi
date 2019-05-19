using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IManageParentTaskDetails
    {
        Task<IEnumerable<ParentTaskDetails>> GetAll();
        Task<ParentTaskDetails> Get(int id);
        Task<int> Insert(ParentTaskDetails parentTaskDetails);
        Task<int> Edit(ParentTaskDetails parentTaskDetails);
        Task<int> Delete(ParentTaskDetails parentTaskDetails);
    }
}
