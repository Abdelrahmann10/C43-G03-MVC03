using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.PL.Models.Departments;

namespace IKEA.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department
            CreateMap<DepartmentDetailsToReturnDTO, DepartmentEditViewModel>()
                .ReverseMap();
            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDTO>();
            #endregion
        }


    }
}
