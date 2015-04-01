using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;

namespace DiceChallengeMVVM.Models
{
    public class GameModel
    {
        public GameModel()
        {
            DiceModels = new List<DiceModel>
            {
                new DiceModel
                {
                    Image = null,
                    Value = 1
                },
                new DiceModel
                {
                    Image = null,
                    Value = 2
                },
                new DiceModel
                {
                    Image = null,
                    Value = 3
                },
                new DiceModel
                {
                    Image = null,
                    Value = 4
                },
                new DiceModel
                {
                    Image = null,
                    Value = 5
                },
                new DiceModel
                {
                    Image = null,
                    Value = 6
                }
            };

            Rules = new List<RuleModel>
            {
                new RuleModel
                {
                    Multiplier = 100,
                    RuleFunc = models => models.GroupBy(x => x.Value).Any(x => x.Count() == 5),
                    Description = "5 of a kind"
                },
                new RuleModel
                {
                    Multiplier = 50,
                    RuleFunc = models => models.GroupBy(x => x.Value).Any(x => x.Count() == 4),
                    Description = "4 of a kind"
                },
                new RuleModel
                {
                    Multiplier = 10,
                    RuleFunc = models => models.GroupBy(x => x.Value).Any(x => x.Count() == 3),
                    Description = "3 of a kind"
                },
                new RuleModel
                {
                    Multiplier = 5,
                    RuleFunc = models => models.GroupBy(x => x.Value).All(x => x.Count() == 1),
                    Description = "Straight"
                }
            };

            Bank = 50;
            DiceToRoll = 5;
        }

        public List<DiceModel> DiceModels { get; private set; }
        public List<RuleModel> Rules { get; private set; }
        public decimal Bank { get; set; }
        public int DiceToRoll { get; private set; }

        public void Reset()
        {
            Bank = 50;
        }
    }
}
