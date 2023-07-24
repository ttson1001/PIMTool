using PIMTool.Core.Dtos.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Dtos.ProjectDtos
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int ProjectNumber { get; set; }
        public List<EmployeeDto>? EmployeeDto { get; set; }
        public int GroupId { get; set; }
        public string Status { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
