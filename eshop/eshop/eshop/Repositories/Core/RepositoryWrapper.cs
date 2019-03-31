using eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Repositories.Core
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private eshopContext _repo;

        private UsersRepository _usersRepo;
        private PicturesRepository _picturesRepo;
        private ArticlesRepository _artRepo;

        public RepositoryWrapper(eshopContext repo)
        {
            _repo = repo;
        }

        public eshopContext Repo
        {
            get
            {
                return _repo;
            }
        }

        public UsersRepository UserRep
        {
            get
            {
                if (_usersRepo == null)
                    _usersRepo = new UsersRepository(_repo);
                return _usersRepo;
            }
        }

        public PicturesRepository PitcuresRep
        {
            get
            {
                if (_picturesRepo == null)
                    _picturesRepo = new PicturesRepository(_repo);
                return _picturesRepo;
            }
        }

        public ArticlesRepository ArtRepo
        {
            get
            {
                if (_artRepo == null)
                    _artRepo = new ArticlesRepository(_repo);
                return _artRepo;
            }
        }

        public void Save()
        {
            _repo.SaveChanges();
        }
    }
}
