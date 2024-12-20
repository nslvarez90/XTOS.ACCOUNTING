using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class AgentWeeksDTO
    {
        public uint IdAgent { set; get; }
        public string? AgentName { set; get; }
        public uint PlayerTotal { set; get; }
        public WeekResumeDTO? Week1 { set; get; }
        public WeekResumeDTO? Week2 { set; get; }
        public WeekResumeDTO? Week3 { set; get; }
        public WeekResumeDTO? Week4 { set; get; }
    }
}
