using eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eshop.Repositories.Core
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        protected eshopContext Repo;

        public RepositoryBase(eshopContext repo)
        {
            Repo = repo;
        }

        public IEnumerable<T> GetAll()
        {
            return Repo.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Repo.Set<T>().Where(expression);
        }

        public T FindOneByCondition(Expression<Func<T, bool>> expression)
        {
            return Repo.Set<T>().SingleOrDefault(expression);
        }

        public T Create(T entity)
        {
            Repo.Set<T>().Add(entity);

            return entity;
        }

        //public T CreateAndSave(T entity)
        //{
        //    Repo.Set<T>().Add(entity);
        //    Save();

        //    return entity;
        //}

        public T Update(T entity)
        {
            Repo.Set<T>().Update(entity);

            return entity;
        }

        //public T UpdateAndSave(T entity)
        //{
        //    Repo.Set<T>().Update(entity);
        //    Save();

        //    return entity;
        //}

        public void Delete(T entity)
        {
            Repo.Set<T>().Remove(entity);
        }

        //public void DeleteAndSave(T entity)
        //{
        //    Repo.Set<T>().Remove(entity);
        //    Save();
        //}

        //public void Save()
        //{
        //    Repo.SaveChanges();
        //}
    }
}
