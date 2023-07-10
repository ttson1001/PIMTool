using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Project
{
    public class ProjectValidateError : Exception
    {
        public ProjectValidateError(string message) :base(message) { }
    }
}
