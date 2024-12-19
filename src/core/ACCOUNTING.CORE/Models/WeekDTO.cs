using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Models
{
    public class WeekDTO
    {
        public double ActivePlayer { set; get; }
        public double Charge { set; get; }
        public double Payment { set; get; }

        public override string ToString()
        {
            return $"{this.ActivePlayer}, {this.Charge}, {this.Payment}";
        }

    }
}
