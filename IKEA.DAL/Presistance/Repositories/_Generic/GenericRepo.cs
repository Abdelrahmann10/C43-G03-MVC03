using IKEA.DAL.Models;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories._Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        private readonly AppDbContext _dbContext;

        public GenericRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                _dbContext.Set<T>().Where(x=>!x.IsDeleted).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().Where(x => !x.IsDeleted).ToList();
        }

        public T? GetById(int id)
        {
            //var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);
            var T = _dbContext.Set<T>().Find(id);
            return T;
        }
        public IQueryable<T> GetAllAsQuarable()
        {
            return _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //Add Local
            //return _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();
        }
    }
}
