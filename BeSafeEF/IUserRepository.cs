using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6CodeFirstDemo
{
    public interface IUserRepository: IGenericRepository<User> 
    {
        IEnumerable<User> ValidateUser(string userName, string password);
    }
}
