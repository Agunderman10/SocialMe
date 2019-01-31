using System;
using System.Windows.Input;

namespace SocialMe
{
    class ButtonCommands : ICommand
    {
        private readonly Action _action; 

        public ButtonCommands(Action action)
        {
            this._action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}
