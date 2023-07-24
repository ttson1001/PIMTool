using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos;
using PIMTool.Core.Dtos.ProjectDtos;
using PIMTool.Core.Dtos.ProjectDtos.Request;
using PIMTool.Validations;
using PIMTool.Core.Exceptions.Project;
using Microsoft.AspNetCore.Cors;
using PIMTool.Core.Constants;

namespace PIMTool.Controllers
{
    [ApiController]
    [Route(Constants.ProjectsRoute)]
    [EnableCors]
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

        [HttpGet(Constants.GetId)]
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
        public async Task<ActionResult<String>> Add(RequestProjectDto project)
        {
            var validate = new ProjectDtoValidator();
            var validationResult = validate.Validate(project);

            if (!validationResult.IsValid)
            {
                throw new ProjectValidateError(validationResult.Errors.First().ErrorMessage);
            }
            var entity = await _projectService.AddAsync(project);

            return Ok(new SendResponseDto
            {
                Data = entity,
                Message = $"Add Project successfull",
                StatusCode = 200
            });
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDto>> Update(RequestProjectDto project)
        {
            var entity = await _projectService.UpdateAsync(project);
            return Ok(new SendResponseDto
            {
                Data = _mapper.Map<ProjectDto>(entity),
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
        public async Task<ActionResult<List<ProjectDto>>> Fillter([FromBody] ProjectSearchDto searchProject)
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
        public async Task<ActionResult<String>> Delete([FromBody] ProjectDeleteDto deleteProject)
        {
            var result = await _projectService.Delete(deleteProject.Ids);
            return Ok(new SendResponseDto
            {
                Data = result,
                Message = "Delete project successfull",
                StatusCode = 200
            });
        }

        [HttpGet("check-project-number/{projectNumber}")]
        public ActionResult<Boolean> CheckProjectNumber(int projectNumber)
        {
            var result = _projectService.CheckProjectNumber(projectNumber);
            return Ok(new SendResponseDto
            {
                Data = result,
                Message = "Delete project successfull",
                StatusCode = 200
            });
        }

    }
}