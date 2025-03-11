using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories._Generic;
using IKEA.DAL.Presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Employees
{
    public class EmployeeRepo : GenericRepo<Employee> ,IEmployeeRepo
    {
        public EmployeeRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
