using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.DAL.Presistance.UnitOfWork;
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
        private readonly IUintOfWork _unitOfWork;

        public DepartmentService(IUintOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartment()
        {
            var departments = _unitOfWork.DepartmentRepo.GetAllAsQuarable().Select(department => new DepartmentToReturnDTO
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
            var department = _unitOfWork.DepartmentRepo.GetById(id);
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
            _unitOfWork.DepartmentRepo.Add(createddepartment);
            return _unitOfWork.Compelete();
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
            _unitOfWork.DepartmentRepo.Update(updateddepartment);
            return _unitOfWork.Compelete();

        }

        public bool DeleteDepartment(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepo;
            var department = departmentRepo.GetById(id);
               if(department is not null)
               {
                  departmentRepo.Delete(department);
               }
            return _unitOfWork.Compelete() > 0;
        }



    }
}
