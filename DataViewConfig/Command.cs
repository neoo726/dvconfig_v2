using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataViewConfig
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        public Command(Action<object>  execute)
        {
            this._execute = execute;
        }
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           _execute(parameter);
        }
    }
}
