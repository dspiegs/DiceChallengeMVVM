using System.Collections.Generic;
using System.Linq;

namespace DiceChallengeMVVM.Models
{
    public class KindRule : Rule
    {
        public int KindCount { get; internal set; }

        public KindRule() { }

        public KindRule(int kindCount)
        {
            KindCount = kindCount;
        }

        public override string Description
        {
            get { return string.Format("{0} of a kind", KindCount); }            
        }

        protected override bool RunRule(IEnumerable<Dice> dice)
        {
            return dice.GroupBy(x => x.Value).Any(x => x.Count() == KindCount);
        }
    }
}