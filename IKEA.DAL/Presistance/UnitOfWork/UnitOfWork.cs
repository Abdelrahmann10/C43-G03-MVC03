using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUintOfWork
    {
        private readonly AppDbContext _dbContext;

        public IEmployeeRepo EmployeeRepo { get
            {
                return new EmployeeRepo(_dbContext);
            }
        }
        public IDepartmentRepo DepartmentRepo { get
            {
                return new DepartmentRepo(_dbContext);
            }
        }
        public UnitOfWork(AppDbContext dbContext)
        {
            //Ask Clr for creating object from applicationDbContext Impilicitky
            _dbContext = dbContext;
        }

        public int Compelete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
