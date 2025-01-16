using System;
using System.Windows.Input;

namespace Torsion.Utils
{
    internal class RelayCommandParam : ICommand
    {
        private Action<object> Action;
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public RelayCommandParam(Action<object> action)
        {
            this.Action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            Action(parameter);
        }
    }
}
