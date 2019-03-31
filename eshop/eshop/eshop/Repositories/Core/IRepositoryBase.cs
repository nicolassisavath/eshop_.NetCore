using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eshop.Repositories.Core
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FindOneByCondition(Expression<Func<T, bool>> expression);
        T Create(T entity);
        //T CreateAndSave(T entity);
        T Update(T entity);
        //T UpdateAndSave(T entity);
        void Delete(T entity);
        //void DeleteAndSave(T entity);
        //void Save();
    }
}
