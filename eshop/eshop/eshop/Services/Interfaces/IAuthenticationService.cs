using eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services.Interfaces
{

    public interface IAuthenticationService
    {
        Users InitializeSaltAndPasswordUser(Users user);
        Users AuthentUser(Users credentials);
    }
}
