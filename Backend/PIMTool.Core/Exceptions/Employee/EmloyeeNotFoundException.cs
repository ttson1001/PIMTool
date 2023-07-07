using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Employee
{
    public class EmployeeNotFoundException : Exception
    { 
        public int? EmployeeId { get; set; }
        public string? Visa { get; set; }

        public EmployeeNotFoundException(string message) : base(message) { }

        public EmployeeNotFoundException(string message, Exception ex) : base(message, ex) { }

        public EmployeeNotFoundException(string message,int employeeId, string visa) : base(message)
        {
            EmployeeId = employeeId;
            Visa = visa;
        }
    }
}
