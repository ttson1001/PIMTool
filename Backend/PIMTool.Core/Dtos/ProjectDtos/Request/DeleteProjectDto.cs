using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Dtos.ProjectDtos.Request
{
    public class DeleteProjectDto
    {
        public List<int> ids { get; set; } = null!;
    }
}

