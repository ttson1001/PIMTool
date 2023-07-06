using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Project;
using PIMTool.Core.Interfaces.Services;
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
        public async Task<ActionResult<ProjectDto>> Get([FromRoute][Required]int id)
        {
            var entity = await _projectService.GetAsync(id);
            return Ok(_mapper.Map<ProjectDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<String>> Add(AddProject project)
        {
            var entity = await _projectService.AddAsync(project);
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult<String>> Update(UpdateProject project)
        {
            var entity = await _projectService.UpdateAsync(project);
            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAll()
        {
            var result = await _projectService.GetAll();
            return Ok(_mapper.Map<List<ProjectDto>>(result));
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<List<ProjectDto>>> Fillter([FromBody] SearchProject searchProject)
        {
            var result = await _projectService.Search(searchProject);
            return Ok(_mapper.Map<List<ProjectDto>>(result));

        }
    }
}