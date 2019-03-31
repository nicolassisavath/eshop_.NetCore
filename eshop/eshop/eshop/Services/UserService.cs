using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services
{
    public class UserService : IUserService
    {
        private IImageService _imageService;
        private IRepositoryWrapper _repoW;

        public UserService(IImageService imageService, IRepositoryWrapper repoW)
        {
            _imageService = imageService;
            _repoW = repoW;
        }

        public Users InitNewUser(Users newUser)
        {
            newUser.UId = Guid.NewGuid().ToString();
            newUser.UDatecreation = DateTime.Now;

            return newUser;
        }
    }
}
