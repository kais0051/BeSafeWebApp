using System;
using System.Linq;
using System.Collections.Generic;
using BeSafeWebApp.DLL;
using BeSafeWebApp.Common;
using Entities = BeSafeWebApp.Contracts.Entities;
using Models = BeSafeWebApp.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;
using BeSafeWebApp.Contracts.Entities;

namespace BeSafeWebApp.DLL
{
    public class MasterItemRepository : GenericRepository<Entities.MasterItemsSet>, IMasterItemRepository
    {
        private BeSafeContext beSafeContext;
        public MasterItemRepository(BeSafeContext context)
            : base(context)
        {
            beSafeContext = context;
        }

        public async Task<IList<MasterItemsSet>> GetMasterItemsByCategoryId(int CategoryId)
        {
            return  beSafeContext.MasterItemsSets.Where(x=>x.CategoryId==CategoryId).ToList();
        }
    }
}
