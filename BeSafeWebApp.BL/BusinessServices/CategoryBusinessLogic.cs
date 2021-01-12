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
    public class CategoryBusinessLogic : ICategoryBusinessLogic
    {
        private ICategoryRepository _categoryRepository;

        public CategoryBusinessLogic(ICategoryRepository categoryRepository)
        {
            if (categoryRepository != null)
                this._categoryRepository = categoryRepository;
        }

        public async Task<IList<Entities.Category>> GetAllCategories()
        {
            return await this._categoryRepository.GetAllCategories();
        }

        public async Task<Entities.Category> GetCategoryById(int id)
        {
            return await this._categoryRepository.GetCategoryById(id);
        }
    }
}
