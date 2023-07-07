using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Project
{
    public class ProjectNotFoundException : Exception
    {
        public int? ProjectId { get; set; }
        public ProjectNotFoundException(string message) : base(message) { }
        public ProjectNotFoundException(string message, Exception  exception) : base(message, exception) { }

        public ProjectNotFoundException(string message, int projectId) : base(message) 
        {
            ProjectId = projectId;
        }
    }
}
