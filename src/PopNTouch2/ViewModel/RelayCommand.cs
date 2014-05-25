using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PopNTouch2.ViewModel
{
    /// <summary>
    /// Inspired by MVVM Light pattern, used to relay easily every command sent from the View
    /// </summary>
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

    /// <summary>
    /// Same as RelayCommand, but handles a parameter of type T
    /// </summary>
    /// <typeparam name="T">Type of the parameter passed via CommandParameter in View</typeparam>
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
