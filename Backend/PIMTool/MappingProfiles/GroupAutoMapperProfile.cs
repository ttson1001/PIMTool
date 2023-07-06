using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos.GroupDtos;

namespace PIMTool.MappingProfiles
{
    public class GroupAutoMapperProfile : Profile
    {
        public GroupAutoMapperProfile()
        {
            CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.EmployeeDTO, opt => opt.MapFrom(src => src.Employee));
        }
    }
}
