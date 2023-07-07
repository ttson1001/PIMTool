using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Employee
{
    public class EmployeeDuplicateVisaException : Exception
    {
        public string? Visa { get; set; } = null;
        public EmployeeDuplicateVisaException(string massage) : base(massage) { }

        public EmployeeDuplicateVisaException(string massage, string visa) : base(massage)
        {
            Visa = visa;
        }

    }
}
