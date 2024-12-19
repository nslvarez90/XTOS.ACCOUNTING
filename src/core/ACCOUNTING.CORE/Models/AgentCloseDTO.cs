using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class AgentCloseDTO
    {
        public uint IdAgent { set; get; }
        public string? AgentName { set; get; }
        public string? RootDGS { set; get; }
        public string? SubAgents { set; get; }
        public double Balance { set; get; }

    }
}
