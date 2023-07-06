using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos;
using PIMTool.Dtos.EmployeeDtos;

namespace PIMTool.MappingProfiles
{
    public class EmployeeAutoMapperProfile : Profile
    {
        public EmployeeAutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
