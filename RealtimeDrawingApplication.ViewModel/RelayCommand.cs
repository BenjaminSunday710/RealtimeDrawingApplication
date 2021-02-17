using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealtimeDrawingApplication.ViewModel
{
    public class RelayCommand:ICommand
    {
        Action _execteMethod;
        Func<bool> _canexecuteMethod;

        public RelayCommand(Action execteMethod, Func<bool> canexecuteMethod)
        {
            _execteMethod = execteMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canexecuteMethod != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Execute(object parameter)
        {
            _execteMethod();
        }
    }
}
