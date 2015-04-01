using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using DiceChallengeMVVM.Annotations;
using DiceChallengeMVVM.Commands;
using DiceChallengeMVVM.Models;

namespace DiceChallengeMVVM.ViewModels
{
    public class DiceViewModel : INotifyPropertyChanged
    {
        private GameModel gm;
        private Random random;
        private decimal betAmount;
        private string errorMessage;

        public ObservableCollection<RuleModel> Rules { get; set; } 
        public ObservableCollection<DiceModel> RolledDice { get; private set; }

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand RollDiceCommand { get; private set; }

        public decimal BetAmount
        {
            get { return betAmount; }
            set
            {
                if (value == betAmount) return;
                betAmount = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (value == errorMessage) return;
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        public DiceViewModel()
        {
            gm = new GameModel();           
            random = new Random();
            RolledDice = new ObservableCollection<DiceModel>();
            Rules = new ObservableCollection<RuleModel>(gm.Rules);

            RollDiceCommand = new DelegateCommand(RollDice, () => Bank > 0);
            NewGameCommand = new DelegateCommand(Reset);
        }

        public void RollDice()
        {
            ErrorMessage = string.Empty;

            if (BetAmount <= 0)
            {
                ErrorMessage = "Please enter an amount greater than zero";
                return;
            }

            if (BetAmount > Bank)
            {
                ErrorMessage = "You do not have enough money";
                return;
            }

            Bank -= BetAmount;

            RolledDice.Clear();
            for (int i = 0; i < gm.DiceToRoll; i++)
            {
                Thread.Sleep(100);
                var diceIndex = random.Next(0, gm.DiceModels.Count - 1);
                RolledDice.Add(gm.DiceModels[diceIndex]);                
            }

            var bestRule = gm.Rules.OrderBy(x => x.Multiplier).FirstOrDefault(rule => rule.RuleFunc(RolledDice));
            if (bestRule != null)
            {
                Bank += BetAmount*bestRule.Multiplier;
            }

            if (Bank == 0)
            {
                ErrorMessage = "Game Over";                
                RollDiceCommand.RefeshCanExecute();
            }
        }

        public void Reset()
        {
            ErrorMessage = string.Empty;
            BetAmount = 0;
            gm.Reset();
            OnPropertyChanged("Bank");
            RollDiceCommand.RefeshCanExecute();
        }


        public decimal Bank
        {
            get { return gm.Bank; }
            set
            {
                gm.Bank = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
