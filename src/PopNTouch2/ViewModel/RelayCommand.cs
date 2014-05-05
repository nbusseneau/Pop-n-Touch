using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

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

    /// <summary>
    /// Just like a regular storyboard, except it calls a command on its completed event
    /// </summary>
    public class CommandStoryboard
    {
        #region CommandParameter

        /// <summary>
        /// CommandParameter Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(CommandStoryboard),
                new FrameworkPropertyMetadata((object)null));

        /// <summary>
        /// Gets the CommandParameter property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static object GetCommandParameter(DependencyObject d)
        {
            return (object)d.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Sets the CommandParameter property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetCommandParameter(DependencyObject d, object value)
        {
            d.SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region Command

        /// <summary>
        /// Command Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandStoryboard),
                new FrameworkPropertyMetadata((ICommand)null,
                    new PropertyChangedCallback(OnCommandChanged)));

        /// <summary>
        /// Gets the Command property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets the Command property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Handles changes to the Command property.
        /// </summary>
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeline = d as Timeline;
            if (timeline != null && !timeline.IsFrozen)
            {
                timeline.Completed += delegate
                {
                    ICommand command = GetCommand(d);
                    object param = GetCommandParameter(d);

                    if (command != null && command.CanExecute(param))
                        GetCommand(d).Execute(GetCommandParameter(d));
                };
            }
        }

        #endregion
    }

    /// <summary>
    /// Timeline that can be used to fire a command
    /// </summary>
    public class CommandTimeline : Storyboard
    {
        /// <summary>
        /// return 1 tick so that we can execute the completed event for this storyboard 
        /// </summary>
        /// <param name="clock"></param>
        /// <returns></returns>
        protected override Duration GetNaturalDurationCore(Clock clock)
        {
            return new Duration(TimeSpan.FromTicks(1));
        }

        /// <summary>
        /// Return a new instance of the CommandTimeline
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new CommandTimeline();
        }
    }
}
