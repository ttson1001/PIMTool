using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using PIMTool.Core.Domain.Objects.Employee;

using PIMTool.Core.Interfaces.Services;
using PIMTool.Dtos;
using PIMTool.Core.Dtos.EmployeeDtos;
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
        public async Task<ActionResult> Add(AddEmployee addEmployee)
        {
            var enity = await _employeeService.AddAsync(addEmployee);
            return Ok(new SendResponseDto
            {
                StatusCode = 200,
                Message = "Add Employee Successfull",
                Data = enity
            });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SendResponseDto>> Get([FromRoute][Required] int id)
        {
            var entity = await _employeeService.GetAsync(id);

            return Ok(new SendResponseDto
            {
                StatusCode = 200,
                Message = "Get Employee Successfull",
                Data = _mapper.Map<EmployeeDto>(entity)
            });

        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateEmployee updateEmployee)
        {
            var enity = await _employeeService.UpdateAsync(updateEmployee);

            return Ok(new SendResponseDto
            {
                StatusCode = 200,
                Message = "Update Employee Successfull",
                Data = enity
            });
        }

        [HttpGet]
        public async Task<ActionResult<SendResponseDto>> GetAll()
        {
            var result = await _employeeService.GetAll();
            return Ok(new SendResponseDto
            {
                StatusCode = 200,
                Message = "Get all employee Successfull",
                Data = _mapper.Map<List<EmployeeDto>>(result)
            });
        }

        [HttpDelete]
        public ActionResult<String> Delete([FromBody] List<int> listId)
        {
            var e = _employeeService.Delete(listId);
            return Ok(e);
        }
    }
}
