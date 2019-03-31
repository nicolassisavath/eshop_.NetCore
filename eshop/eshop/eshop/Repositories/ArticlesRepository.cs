using eshop.Models;
using eshop.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Repositories
{
    public class ArticlesRepository : RepositoryBase<Articles>
    {
        public ArticlesRepository(eshopContext context)
        : base(context)
        {
        }
    }
}
