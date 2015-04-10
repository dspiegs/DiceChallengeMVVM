using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceChallengeMVVM.Models
{
    public class CustomRule : Rule
    {
        public Func<IEnumerable<Dice>, bool> RuleFunc { private get; set; }

        protected override bool RunRule(IEnumerable<Dice> dice)
        {
            return RuleFunc(dice);
        }
    }
}
