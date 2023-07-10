using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Dtos.ProjectDtos.Request
{
    public class SearchProjectDto
    {
        public string? ProjectName { get; set; }

        public int? ProjectNumber { get; set; }

        public string? Status { get; set; }

        public string? Customer { get; set; }
    }
}
