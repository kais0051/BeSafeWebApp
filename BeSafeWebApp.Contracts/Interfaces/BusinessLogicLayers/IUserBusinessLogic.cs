using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BeSafeWebApp.Contracts.Interfaces
{
    public interface IUserBusinessLogic
    {
        Task<IList<Entities.User>> GetUsers();
        Task<Entities.User> GetUserById(int id);
        Task<Entities.User> UserValidation(string userName, string password);
    }

}