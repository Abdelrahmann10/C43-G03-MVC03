using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentRepo;
        public DepartmentService(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartment()
        {
            var departments = _departmentRepo.GetAllAsQuarable().Select(department => new DepartmentToReturnDTO
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToList();
            return departments;
        }

        public DepartmentDetailsToReturnDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepo.GetById(id);
            if (department is not null)
            {
                return new DepartmentDetailsToReturnDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModificationBy = department.LastModificationBy,
                    LastModificationOn = department.LastModificationOn,
                    IsDeleted = department.IsDeleted,
                };
            }
            return null;
        }

        public int CreateDepartment(CreatedDepartmentDTO departmentDTO)
        {
            var createddepartment = new Department()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
                //CreatedOn = DateTime.UtcNow,
            };
            return _departmentRepo.Add(createddepartment);
        }

        public int UpdateDepartment(UpdatedDepartmentDTO departmentDTO)
        {
            var updateddepartment = new Department()
            {
                Id = departmentDTO.Id,
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow
            };
            return _departmentRepo.Update(updateddepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepo.GetById(id);
               if(department is not null)
               {
                  return _departmentRepo.Delete(department) > 0;
               }
               return false;
        }



    }
}
