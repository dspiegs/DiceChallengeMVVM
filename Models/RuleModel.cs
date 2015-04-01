using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceChallengeMVVM.Models
{
    public class RuleModel
    {
        public int Multiplier { get; set; }
        public Func<IEnumerable<DiceModel>, bool> RuleFunc { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}x bet", Description, Multiplier);
        }
    }
}
