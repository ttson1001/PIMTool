using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class ProjectEmployee :IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public Project? Project { get; set; }

        public Employee? Employee { get; set; }
    }
}
