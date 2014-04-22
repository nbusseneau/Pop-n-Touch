using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    public class RelayCommand : ICommand
    {
        private Action execute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (execute != null)
                execute();
        }

    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> execute;

        public RelayCommand(Action<T> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is T;
        }

        public event EventHandler CanExecuteChanged;

        [System.Diagnostics.DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (execute != null)
                execute((T)parameter);
        }

    }
}
