using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public interface IUintOfWork : IDisposable
    {
        public IEmployeeRepo EmployeeRepo { get; }
        public IDepartmentRepo DepartmentRepo { get; }

        int Compelete();
    }
}
