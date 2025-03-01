using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepo
    {
        IEnumerable<Department> GetAll(bool WithAsNoTracking = true);
        IQueryable<Department> GetAllAsQuarable();
        Department? GetById(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);

    }
}
