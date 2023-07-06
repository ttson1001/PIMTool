using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Group;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos.GroupDtos;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Controllers
{
    [Route("groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<GroupDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _groupService.GetAsync(id);
            return Ok(_mapper.Map<GroupDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<String>> Add([FromBody] AddGroup addGroup)
        {
            var entity = await _groupService.AddAsync(addGroup);
            return Ok(entity);

        }

        [HttpPut]
        public async Task<ActionResult<GroupDto>> Update([FromBody] UpdateGroup updateGroup)
        {
            var entity = await _groupService.UpdateAsync(updateGroup);

            return Ok(_mapper.Map<GroupDto>(entity));
            
        }

        [HttpGet]

        public async Task<ActionResult<List<Group>>> GetAll()
        {
            var response = await _groupService.GetAllAsync();
            return Ok(_mapper.Map<List<GroupDto>>(response));
        }
    }
}
