using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class AgentInfoDTO
    {
        public uint IdAgent { set; get; }
        public string? AgentName { set; get; }
        public double Fee { set; get; }
        public double Balance { set; get; }
        public List<WeekDTO> WeeksInfo { set; get; } = new List<WeekDTO>();
        public double TotalPayment { set; get; }
        public double BalancePeriod { set; get; }

    }
}
