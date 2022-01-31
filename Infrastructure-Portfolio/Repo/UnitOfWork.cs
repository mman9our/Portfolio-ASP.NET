using Core_Portfolio.Interface;
using Infrastructure_Portfolio.repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure_Portfolio.Repo
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepo<T> _entity;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepo<T> Entity
        {
            get
            {
                if (_entity != null)
                {
                    return _entity;
                }
                    
                else
                    return _entity = new GenericRepo<T>(_context);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
