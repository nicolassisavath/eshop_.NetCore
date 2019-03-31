using eshop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services.Core
{
    public interface IServicesWrapper
    {
        IArticlesService ArticleSvc { get; }
        IAuthenticationService AuthentSvc { get; }
        IImageService ImageSvc { get; }
        ITokenService TokenSvc { get; }
        IUserService UserSvc { get;  }
    }
}
