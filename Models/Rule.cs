using System.Collections.Generic;
using System.Linq;

namespace DiceChallengeMVVM.Models
{
    public abstract class Rule
    {
        public int Multiplier { get; internal set; }
        public virtual string Description { get; internal set; }

        public bool PassesRule(IEnumerable<Dice> dice)
        {
            if (dice == null || !dice.Any())
            {
                return false;
            }

            return RunRule(dice);
        }

        protected abstract bool RunRule(IEnumerable<Dice> dice);

        public override string ToString()
        {
            return string.Format("{0} - {1}x bet", Description, Multiplier);
        }
    }
}