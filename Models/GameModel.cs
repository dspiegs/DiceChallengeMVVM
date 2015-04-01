using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using DiceChallengeMVVM.Properties;

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
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice1.gif")),
                    Value = 1
                },
                new DiceModel
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice2.gif")),
                    Value = 2
                },
                new DiceModel
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice3.gif")),
                    Value = 3
                },
                new DiceModel
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice4.gif")),
                    Value = 4
                },
                new DiceModel
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice5.gif")),
                    Value = 5
                },
                new DiceModel
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice6.gif")),
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
                    RuleFunc = models =>
                    {
                        var runLength = 0;
                        var values = models.Select(x => x.Value).Distinct().OrderBy(x => x).ToList();
                        foreach (var value in values)
                        {
                            if (values.Any(x => x == value + 1))
                            {
                                runLength ++;
                            }
                            
                            if (runLength == 4)
                            {
                                return true;
                            }
                        }
                        return false;
                    },
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
