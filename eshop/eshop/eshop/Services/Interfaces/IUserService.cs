using eshop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services.Interfaces
{
    public interface IUserService
    {
        Users InitNewUser(Users newUser);
    }
}
