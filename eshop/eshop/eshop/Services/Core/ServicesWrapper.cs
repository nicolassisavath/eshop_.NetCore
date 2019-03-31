using eshop.Services.Core;
using eshop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services
{
    public class ServicesWrapper : IServicesWrapper
    {
        private IArticlesService _articleSvc;
        private IAuthenticationService _authentSvc;
        private IImageService _imageSvc;
        private ITokenService _tokenSvc;
        private IUserService _userSvc;

        public IArticlesService ArticleSvc { get => _articleSvc; }
        public IAuthenticationService AuthentSvc { get =>_authentSvc; }
        public IImageService ImageSvc { get => _imageSvc; }
        public ITokenService TokenSvc { get => _tokenSvc; }
        public IUserService UserSvc { get => _userSvc; }

        public ServicesWrapper(
            IArticlesService articleSvc,
            IAuthenticationService authentSvc,
            IImageService imageSvc,
            ITokenService tokenSvc,
            IUserService userSvc)
        {
            _articleSvc = articleSvc;
            _authentSvc = authentSvc;
            _imageSvc = imageSvc;
            _tokenSvc = tokenSvc;
            _userSvc = userSvc;
        }
    }
}
