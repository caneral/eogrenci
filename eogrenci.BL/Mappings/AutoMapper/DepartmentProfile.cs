using AutoMapper;
using eogrenci.Dtos.DepartmentDtos;
using eogrenci.Entities.Concrete;

namespace eogrenci.BL.Mappings.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentListDto>().ReverseMap();
            CreateMap<Department, DepartmentAddDto>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();
        }
    }
}
