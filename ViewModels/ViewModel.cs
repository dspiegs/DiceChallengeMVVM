using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DiceChallengeMVVM.Annotations;
using DiceChallengeMVVM.Commands;
using DiceChallengeMVVM.Models;

namespace DiceChallengeMVVM.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly Game gm;
        private readonly Random random;

        private decimal betAmount;
        private string errorMessage;

        public ViewModel()
        {
            gm = new Game();
            random = new Random();
            RolledDice = new ObservableCollection<Dice>();
            Rules = new ObservableCollection<Rule>(gm.Rules);

            RollDiceCommand = new DelegateCommand(RollDice, () => Bank > 0);
            NewGameCommand = new DelegateCommand(Reset);
        }

        public ObservableCollection<Rule> Rules { get; set; }
        public ObservableCollection<Dice> RolledDice { get; private set; }

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

        public void RollDice()
        {
            ErrorMessage = string.Empty;

            if (BetAmount <= 0)
            {
                ErrorMessage = "Please enter an amount greater than zero.";
                return;
            }

            if (BitConverter.GetBytes(decimal.GetBits(BetAmount)[3])[2] > 2)
            {
                ErrorMessage = "You cannot bet with a fraction of a penny.";
                return;
            }

            if (BetAmount > Bank)
            {
                ErrorMessage = "You do not have enough money.";
                return;
            }

            RolledDice.Clear();
            for (int i = 0; i < gm.DiceToRoll; i++)
            {
                var diceIndex = random.Next(0, gm.DiceModels.Count - 1);
                RolledDice.Add(gm.DiceModels[diceIndex]);
            }

            var bestRule = gm.Rules.OrderBy(x => x.Multiplier).FirstOrDefault(rule => rule.PassesRule(RolledDice));
            if (bestRule != null)
            {
                Bank += BetAmount*bestRule.Multiplier;
            }
            else
            {
                Bank -= BetAmount;
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}