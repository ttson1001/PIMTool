using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Dtos.ProjectDtos;
using PIMTool.Core.Dtos.ProjectDtos.Request;

namespace PIMTool.MappingProfiles
{
    public class ProjectAutoMapperProfile : Profile
    {
        public ProjectAutoMapperProfile()
        {

            CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.EmployeeDto, opt => opt.MapFrom(src => src.projectEmployees.Select(pe => pe.Employee)));


            CreateMap<RequestProjectDto, Project>()
                .ForMember(dest => dest.GroupId, opt => opt.Ignore());
        }
    }
}