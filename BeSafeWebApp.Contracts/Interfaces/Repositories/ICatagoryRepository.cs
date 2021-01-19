using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Entities;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Entities.Category>
    {
        Task<IList<Entities.Category>> GetAllCategories();
        Task<Entities.Category> GetCategoryById(long id);

        //Task<int> AddUser(Entities.User inputEt);
        //Task UpdateUser(Entities.User inputEt);
        //Task DeleteUser(int id);
        Task<IList<Entities.Category>> GetCategories(long id);
    }
}
