using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceChallengeMVVM.Models
{
    public interface IRule
    {
        int Multiplier { get; }
        bool PassesRule(IEnumerable<Dice> dice);
        string Description { get; }
    }

    public abstract class Rule : IRule
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
