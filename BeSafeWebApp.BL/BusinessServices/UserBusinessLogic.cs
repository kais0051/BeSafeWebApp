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
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private IUserRepository _userRepository;

        public UserBusinessLogic(IUserRepository userRepository)
        {
            if (userRepository != null)
                this._userRepository = userRepository;
        }

        public async Task<IList<Entities.User>> GetUsers()
        {
            return await this._userRepository.GetUsers();
        }

        public async Task<Entities.User> GetUserById(int id)
        {
            return await this._userRepository.GetUserById(id);
        }

        public async Task<User> UserValidation(string userName, string password)
        {
            return await this._userRepository.UserValidation(userName, password);
        }
        public async Task<Entities.User> AddUser(Entities.User user)
        {
           return await this._userRepository.InsertAsync(user, true);
        }
    }
}
