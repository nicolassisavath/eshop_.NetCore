using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services
{
    public class ArticlesService : IArticlesService
    {
        private IRepositoryWrapper _repoW;

        public ArticlesService(IRepositoryWrapper repoW)
        {
            _repoW = repoW;
        }

        public Articles CreateArticle(Articles article)
        {
            article = InitializeArticle(article);
            _repoW.ArtRepo.Create(article);

            return article;
        }

        private Articles InitializeArticle(Articles article)
        {
            article.AId = Guid.NewGuid().ToString();

            return article;
        }
    }
}
