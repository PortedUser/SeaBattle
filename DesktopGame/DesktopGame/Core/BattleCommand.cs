using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopGame.Core
{
    class BattleCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            private set { x = value; }
        }

        public int Y
        {
            get { return y; }
            private set { y = value; }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public BattleCommand(Action<object> execute, int _x, int _y, Func<object, bool> canExecute)
        {
            X = _x;
            Y = _y;
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
