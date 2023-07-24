using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Dtos.ProjectDtos.Request
{
    public class RequestProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProjectNumber { get; set; }

        public string Customer { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime ?EndDate { get; set; }

        public int GroupId { get; set; }

        public string ?Members { get; set; }
    }
}
