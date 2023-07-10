using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Dtos.EmployeeDtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Visa { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime Birthday { get; set; }

    }
}
