using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class DuplicateInfoDTO
    {
        public uint IdAgentMain { set; get; }
        public string? MainAgentName { set; get; }
        public uint IdAgentDuplicate { set; get; }
        public string? DuplicateAgentName { set; get; }
        public CoincidenceDTO? Ips { set; get; }
        public CoincidenceDTO? Password { set; get; }
        public CoincidenceDTO? Bets { set; get; }
        public CoincidenceDTO? Hours { set; get; }

    }
}
