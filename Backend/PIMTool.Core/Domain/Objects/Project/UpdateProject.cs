using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Objects.Project
{
    public class UpdateProject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Customer { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }

        public String Members { get; set; }
    }
}
