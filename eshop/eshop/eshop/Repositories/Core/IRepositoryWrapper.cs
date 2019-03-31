using eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Repositories.Core
{
    public interface IRepositoryWrapper
    {
        eshopContext Repo { get; }

        UsersRepository UserRep { get; }
        PicturesRepository PitcuresRep { get; }
        ArticlesRepository ArtRepo { get; }

        void Save();
    }
}
