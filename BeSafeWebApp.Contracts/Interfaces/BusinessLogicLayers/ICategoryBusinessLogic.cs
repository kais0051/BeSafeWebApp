using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface ICategoryBusinessLogic
    {
        Task<IList<Entities.Category>> GetAllCategories();
        Task<Entities.Category> GetCategoryById(long id);
        Task<Entities.Category> AddCategory(Entities.Category category);
        Task<Entities.Category> UpdateCategory(Entities.Category category);
        Task DeleteCategory(Entities.Category category);
    }

}