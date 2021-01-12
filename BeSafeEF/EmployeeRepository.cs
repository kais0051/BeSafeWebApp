using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6CodeFirstDemo
{
  public  class UserRepository: GenericRepository<User>, IUserRepository
    {
        public IEnumerable<User> ValidateUser(string userName, string password)
        {
            return _context.User.Where(m => m.UserName == userName && m.Password==password).ToList();
        }
    }
}
