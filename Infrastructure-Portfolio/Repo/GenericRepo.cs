using Core_Portfolio.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure_Portfolio.repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;


        public GenericRepo(DataContext Context)
        {
            _context = Context;
            table = _context.Set<T>();
        }

        public DataContext DataContext { get; }

        public void Delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }


}