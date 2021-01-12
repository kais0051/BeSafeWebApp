using System;
using System.Linq;
using System.Collections.Generic;
using BeSafeWebApp.DLL;
using BeSafeWebApp.Common;
using Entities = BeSafeWebApp.Contracts.Entities;
using Models = BeSafeWebApp.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BeSafeWebApp.Contracts.Interfaces;

namespace BeSafeWebApp.DLL
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        private BeSafeContext beSafeContext;
        public UserRepository(BeSafeContext context)
            : base(context)
        {
            beSafeContext = context;
        }

        public async Task<IList<Entities.User>> GetUsers()
        {
            return await this.GetAllAsync();
        }

        public async Task<Entities.User> GetUserById(int id)
        {
            return await this.GetByIdAsync(id);
        }

        public async Task<Entities.User> UserValidation(string userName, string password)
        {
            var user = beSafeContext.Users.Where(x => x.UserName.Trim() == userName.Trim() && x.Password == password.Trim() && x.IsActive==true).FirstOrDefault();
            return user;
        }

    }
}
