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

        public async Task<Entities.Category> GetCategoryById(long id)
        {
            return await this._categoryRepository.GetCategoryById(id);
        }

        public async Task<Entities.Category> AddCategory(Entities.Category category)
        {
            var result= await this._categoryRepository.InsertAsync(category,true);
            return category;
        }

        public async Task<Entities.Category> UpdateCategory(Entities.Category category)
        {
            await this._categoryRepository.UpdateAsync(category,true);
            return category;
        }

        public async Task DeleteCategory(Entities.Category category)
        {
            await this._categoryRepository.DeleteAsync(category, true);
        }

        public async Task<IList<Entities.Category>> GetCategories(int idcategory)
        {
            return await this._categoryRepository.GetCategories(idcategory);
        }

        
    }
}
