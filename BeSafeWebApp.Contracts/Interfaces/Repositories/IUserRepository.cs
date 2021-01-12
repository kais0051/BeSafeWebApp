using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface IUserRepository : IGenericRepository<Entities.User>
    {
        Task<IList<Entities.User>> GetUsers();
        Task<Entities.User> GetUserById(int id);
        Task<Entities.User> UserValidation(string userName, string password);

        //Task<int> AddUser(Entities.User inputEt);
        //Task UpdateUser(Entities.User inputEt);
        //Task DeleteUser(int id);
    }
}
