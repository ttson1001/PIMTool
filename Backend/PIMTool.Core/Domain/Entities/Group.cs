using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class Group : IEntity 
    {
        public int Id { get; set; }

        public int? GroupLeaderId { get; set; }

        public byte[] Version { get; set; }

        public Employee Employee { get; set; } = null!;

        public ICollection<Project> Projects { get; set; }
    }
}
