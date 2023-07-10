using PIMTool.Core.Dtos.EmployeeDtos;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Dtos.GroupDtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public EmployeeDto? EmployeeDTO { get; set; }
    }
}
