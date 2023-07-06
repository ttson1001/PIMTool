using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Domain.Objects.Employee;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos.EmployeeDtos;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<String>> Add(AddEmployee addEmployee)
        {
            try
            {
                var enity = await _employeeService.AddAsync(addEmployee);
                return Ok(enity);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _employeeService.GetAsync(id);
            return Ok(_mapper.Map<EmployeeDto>(entity));
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeDto>> Update([FromBody] UpdateEmployee updateEmployee)
        {
            var entity = await _employeeService.UpdateAsync(updateEmployee);
            return Ok(_mapper.Map<EmployeeDto>(entity));
        }

        [HttpGet]
        public ActionResult<EmployeeDto> GetAll()
        {
            var e = _employeeService.GetAll();
            return Ok(_mapper.Map<List<EmployeeDto>>(e));
        }

        [HttpDelete]
        public ActionResult<String> Delete([FromBody]List<int> listId)
        {
            var e = _employeeService.Delete(listId);
            return Ok(e);
        }
    }
}
