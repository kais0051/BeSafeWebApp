using System.Collections.Generic;
using System;
using System.Linq;
using BeSafeWebApp.Common;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;
using Entities = BeSafeWebApp.Contracts.Entities;
using Models = BeSafeWebApp.Contracts.Models;
using BeSafeWebApp.Contracts.Entities;

namespace BeSafeWebApp.BLL
{
    public class MasterItemBusinessLogic : IMasterItemBusinessLogic
    {
        private IMasterItemRepository _masterItemRepository;

        public MasterItemBusinessLogic(IMasterItemRepository masterItemRepository)
        {
            if (masterItemRepository != null)
                this._masterItemRepository = masterItemRepository;
        }

        public async Task<IList<MasterItemsSet>> GetAllMasterItems()
        {
            return await this._masterItemRepository.GetAllAsync();
        }

        public async Task<MasterItemsSet> GetMasterItemById(int id)
        {
            return await this._masterItemRepository.GetByIdAsync(id);
        }

        public async Task<IList<MasterItemsSet>> GetMasterItemsByCategoryId(int id)
        {
            return await this._masterItemRepository.GetMasterItemsByCategoryId(id);
        }

        public async Task AddMasterItem(MasterItemsSet itemsSet)
        {
             await this._masterItemRepository.InsertAsync(itemsSet,true);
        }

        public async Task UpdateMasterItem(MasterItemsSet masterItemsSet)
        {
            await this._masterItemRepository.UpdateAsync(masterItemsSet, true);
        }

        public async Task DeleteMasterItem(MasterItemsSet masterItemsSet)
        {
            await this._masterItemRepository.DeleteAsync(masterItemsSet, true);
        }
    }
}
