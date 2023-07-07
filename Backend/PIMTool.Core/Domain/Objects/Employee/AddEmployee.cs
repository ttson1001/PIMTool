using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Objects.Employee
{
    public class AddEmployee
    {
        [StringLength(3, ErrorMessage = "Maximum 3 character")]
        [RegularExpression(@"^[A-Z]*$", ErrorMessage = "Just only letter")]
        public string Visa { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Length maximum is 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Just only letter")]
        public string FirstName { get; set; } = string.Empty!;

        [StringLength(50, ErrorMessage = "Length maximum is 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Just only letter")]
        public string LastName { get; set; } = string.Empty!;

        [Required]
        public DateTime Birthday { get; set; }

    }
}
