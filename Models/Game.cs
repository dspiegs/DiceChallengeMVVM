using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using DiceChallengeMVVM.Properties;

namespace DiceChallengeMVVM.Models
{
    public class Game
    {
        public Game()
        {
            DiceModels = new List<Dice>
            {
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice1.gif")),
                    Value = 1
                },
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice2.gif")),
                    Value = 2
                },
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice3.gif")),
                    Value = 3
                },
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice4.gif")),
                    Value = 4
                },
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice5.gif")),
                    Value = 5
                },
                new Dice
                {
                    BitmapImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Dice6.gif")),
                    Value = 6
                }
            };

            Rules = GetRules();

            Bank = 50;
            DiceToRoll = 5;
        }

        public static List<IRule> GetRules()
        {
            return new List<IRule>
            {
                new KindRule(5){Multiplier = 100},
                new KindRule(4){Multiplier = 50},
                new KindRule(3){Multiplier = 10},
                new CustomRule
                {
                    Multiplier = 5,
                    RuleFunc = models =>
                    {
                        var diceModels = models as IList<Dice> ?? models.ToList();
                                              
                        //a straight must be as long as the number of dice
                        var reqLength = diceModels.Count();

                        //if only one die, then there is a straight
                        if (reqLength == 1)
                        {
                            return true;
                        }

                        //only need to know the values to know if there is a straight
                        //put in order so only one traversal is required
                        var values = diceModels.Select(x => x.Value).Distinct().OrderBy(x => x).ToList();

                        //if there are less values than required there cannot be a straight
                        if (values.Count < reqLength)
                        {
                            return false;
                        }

                        var runLength = 1; // there is always a run of at least 1                        
                        for (int i = 0; i < values.Count - 1; i++)
                        {
                            if(values[i + 1] == values[i] + 1)
                            {
                                runLength ++;
                            }

                            if (runLength == reqLength)
                            {
                                return true;
                            }
                        }
                        return false;
                    },
                    Description = "Straight"
                }
            };
        }

        public List<Dice> DiceModels { get; private set; }
        public List<IRule> Rules { get; private set; }
        public decimal Bank { get; set; }
        public int DiceToRoll { get; private set; }

        public void Reset()
        {
            Bank = 50;
        }
    }
}
