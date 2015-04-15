using System;
using System.Windows.Input;
using DiceChallengeMVVM.Annotations;

namespace DiceChallengeMVVM.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> canExecuteFunc;
        private bool canExecute = true;
        private bool canExecuteSet = false;

        public DelegateCommand([NotNull] Action action, [NotNull] Func<bool> canExecuteFunc)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (canExecuteFunc == null)
            {
                throw new ArgumentNullException("canExecuteFunc");
            }

            this.action = action;
            this.canExecuteFunc = canExecuteFunc;
        }

        public DelegateCommand([NotNull] Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            this.action = action;
            canExecuteFunc = () => true;
            canExecuteSet = true;
        }

        public void Execute(object parameter)
        {
            action();
        }

        public bool CanExecute(object parameter)
        {
            if (!canExecuteSet)
            {
                RefeshCanExecute();
                canExecuteSet = true;
            }

            return canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void RefeshCanExecute()
        {
            var canExNew = canExecuteFunc();
            if (canExNew == canExecute)
            {
                return;
            }

            canExecute = canExNew;
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}