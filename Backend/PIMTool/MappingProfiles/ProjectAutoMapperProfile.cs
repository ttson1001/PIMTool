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
            CreateMap<Project, ProjectDto>();

            CreateMap<UpdateProjectDto, Project>();
        }
    }
}