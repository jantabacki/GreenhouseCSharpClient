using System;
using System.Windows.Input;

namespace GreenhouseUIClient.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void OnCanExexuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action<T> execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action<T> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute?.Invoke((T)parameter);
        }
    }

    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void OnCanExexuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute?.Invoke();
        }
    }
}
