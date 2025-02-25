using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Departments
{
    internal class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepo(AppDbContext dbContext)
        // Ask CLr for object from AppDbContext implicitly
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if(WithAsNoTracking)
            {
                _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            //var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            var department = _dbContext.Departments.Find(id);
            return department;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            //Add Local
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
