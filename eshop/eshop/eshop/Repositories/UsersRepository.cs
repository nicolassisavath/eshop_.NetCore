using eshop.Models;
using eshop.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Repositories
{
    public class UsersRepository : RepositoryBase<Users>
    {

        public UsersRepository(eshopContext repo)
        : base(repo)
        {
        }

        public Users GetUserBylogin(string login)
        {
            Users user = FindOneByCondition(u => u.ULogin.Equals(login));

            return user;
        }

        public bool ExistUserWithLogin(string login)
        {
            return FindOneByCondition(u => u.UId.Equals(login)) != null;
        }
    }
}
