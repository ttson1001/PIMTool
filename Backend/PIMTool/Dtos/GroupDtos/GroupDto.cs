using PIMTool.Dtos.EmployeeDtos;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Dtos.GroupDtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public EmployeeDto EmployeeDTO { get; set; }
    }
}
