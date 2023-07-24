using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Project
{
    public class ProjectDupicateNumberException : Exception
    {
        public int? ProjectNumber { get; set; }

        public ProjectDupicateNumberException(string message) : base(message) { }

        public ProjectDupicateNumberException(string message, Exception exception) : base(message, exception) { }

        public ProjectDupicateNumberException(string message, int projectNumber) : base(message) 
        {
            ProjectNumber = projectNumber;
        }
    }
}
