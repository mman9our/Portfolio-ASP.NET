using System;
using System.Collections.Generic;
using System.Text;

namespace Core_Portfolio.Interface
{
    public  interface IGenericRepo<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
