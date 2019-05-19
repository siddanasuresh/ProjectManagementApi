using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BusinessLogic
{
    public class ManageParentTaskDetails : IManageParentTaskDetails
    {
        IParentTaskDetails _parentTaskDetails;

        public ManageParentTaskDetails(IParentTaskDetails parentTaskDetails)
        {
            _parentTaskDetails = parentTaskDetails;
        }
        public async Task<IEnumerable<ParentTaskDetails>> GetAll()
        {
          return await _parentTaskDetails.GetAll();
        }
        public async Task<Entities.ParentTaskDetails> Get(int id)
        {
            return await _parentTaskDetails.Get(id);
        }
        public async Task<int> Insert(ParentTaskDetails parentTaskDetails)
        {
            return await _parentTaskDetails.Insert(parentTaskDetails);
        }
        public async Task<int> Edit(ParentTaskDetails parentTaskDetails)
        {
            return await _parentTaskDetails.Edit(parentTaskDetails);
        }
        public async Task<int> Delete(ParentTaskDetails parentTaskDetails)
        {
            return await _parentTaskDetails.Delete(parentTaskDetails);
        }
    }
}
