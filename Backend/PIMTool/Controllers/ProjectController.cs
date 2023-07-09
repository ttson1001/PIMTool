using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Project;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos;
using PIMTool.Dtos.ProjectDtos;

namespace PIMTool.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _projectService.GetAsync(id);

            return Ok(new SendResponseDto
            {
                Data = _mapper.Map<ProjectDto>(entity),
                Message = $"Find Project with {id} successfull",
                StatusCode = 200
            });
        }

        [HttpPost]
        public async Task<ActionResult<String>> Add(AddProject project)
        {
            var entity = await _projectService.AddAsync(project);
            return Ok(new SendResponseDto
            {
                Data = entity,
                Message = $"Add Project successfull",
                StatusCode = 200
            });
        }

        [HttpPut]
        public async Task<ActionResult<String>> Update(UpdateProject project)
        {
            var entity = await _projectService.UpdateAsync(project);
            return Ok(new SendResponseDto
            {
                Data = entity,
                Message = $"Update Project successfull",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAll()
        {
            var result = await _projectService.GetAll();
            return Ok(new SendResponseDto
            {
                Data = _mapper.Map<List<ProjectDto>>(result),
                Message = "Find all project successfull",
                StatusCode = 200
            });
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<List<ProjectDto>>> Fillter([FromBody] SearchProject searchProject)
        {
            var result = await _projectService.Search(searchProject);
            return Ok(new SendResponseDto
            {
                Data = _mapper.Map<List<ProjectDto>>(result),
                Message = "Search project successfull",
                StatusCode = 200
            });

        }

        [HttpDelete]
        public async Task<ActionResult<String>> Delete([FromBody] DeleteProject deleteProject)
        {
            var result = await _projectService.Delete(deleteProject.ids);
            return Ok(new SendResponseDto
            {
                Data = result,
                Message = "Delete project successfull",
                StatusCode = 200
            });
        }

    }
}