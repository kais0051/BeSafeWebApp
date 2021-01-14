using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Entities.Category>
    {
        Task<IList<Entities.Category>> GetAllCategories();
        Task<Entities.Category> GetCategoryById(long id);

        //Task<int> AddUser(Entities.User inputEt);
        //Task UpdateUser(Entities.User inputEt);
        //Task DeleteUser(int id);
    }
}
