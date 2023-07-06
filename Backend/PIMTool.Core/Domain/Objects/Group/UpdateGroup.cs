using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Objects.Group
{
    public class UpdateGroup
    {
        public int Id { get; set; }

        public int GroupLeaderId { get; set; }
    }
}
