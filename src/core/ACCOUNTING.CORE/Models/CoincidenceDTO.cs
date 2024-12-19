using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class CoincidenceDTO
    {
        public uint? TotalMatch { set; get; }
        public double? MatchPercent { set; get; }
        public string? RelatedInfo { set; get; }

        public override string ToString()
        {
            return $"{this.TotalMatch.ToString()}, {this.MatchPercent.ToString()}, {this.RelatedInfo}";
        }
    }

}
