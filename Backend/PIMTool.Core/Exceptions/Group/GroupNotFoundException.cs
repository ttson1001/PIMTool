using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Exceptions.Group
{
    public class GroupNotFoundException : Exception
    {
        public int? GroupId { get; set; }

        public GroupNotFoundException(string message) : base(message) { }

        public GroupNotFoundException(string message, Exception ex) : base(message, ex) { }

        public GroupNotFoundException(string message, int groupId) : base(message)
        {
               GroupId = groupId;
        }
    }
}
