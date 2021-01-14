using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface IMasterItemBusinessLogic
    {
        Task<IList<Entities.MasterItemsSet>> GetAllMasterItems();
        Task<IList<Entities.MasterItemsSet>> GetMasterItemsByCategoryId(int CategoryId);
        Task<Entities.MasterItemsSet> GetMasterItemById(int id);
        Task AddMasterItem(Entities.MasterItemsSet category);
        Task UpdateMasterItem(Entities.MasterItemsSet category);
        Task DeleteMasterItem(Entities.MasterItemsSet category);
    }

}