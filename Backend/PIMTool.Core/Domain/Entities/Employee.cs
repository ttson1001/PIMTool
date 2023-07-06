using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public string Visa { get; set; }
        public string FirstName { get; set; } = string.Empty!;

        public string LastName { get; set; } = string.Empty!;

        public DateTime Birthday { get; set; }

        public byte[] Version { get; set; }

        public Group? Group { get; set; }

        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }

    }
}
