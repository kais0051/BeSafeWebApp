using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface IMasterItemRepository : IGenericRepository<Entities.MasterItemsSet>
    {
        Task<IList<Entities.MasterItemsSet>> GetMasterItemsByCategoryId(long CategoryId);

    }
}
