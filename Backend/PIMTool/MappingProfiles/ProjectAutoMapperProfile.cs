using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos.EmployeeDtos;
using PIMTool.Dtos.ProjectDtos;

namespace PIMTool.MappingProfiles
{
    public class ProjectAutoMapperProfile : Profile
    {
        public ProjectAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}