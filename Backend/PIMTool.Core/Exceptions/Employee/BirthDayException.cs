using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Employee
{
    public class BirthDayException : Exception
    {
        public int? EmployeeID { get; set; }
        public DateTime BirthDay { get; set; }
        public BirthDayException(string message) : base(message) { }

        public BirthDayException(string message, DateTime birthday) : base(message) {
            BirthDay = birthday;
        }

        public BirthDayException(string message, int employeeId, DateTime birthDay) : base(message)
        {
            EmployeeID = employeeId;
            BirthDay = birthDay;
        }
    }
}
