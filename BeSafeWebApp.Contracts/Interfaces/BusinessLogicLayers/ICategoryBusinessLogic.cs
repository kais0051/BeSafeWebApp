using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface ICategoryBusinessLogic
    {
        Task<IList<Entities.Category>> GetAllCategories();
        Task<Entities.Category> GetCategoryById(int id);

    }

}