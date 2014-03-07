using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private Visibility startButtonVisibility = Visibility.Visible;
        public Visibility StartButtonVisibility
        {
            get
            {
                return this.startButtonVisibility;
            }
        }

        ICommand startGame;
        public ICommand StartGame
        {
            get
            {
                if (startGame == null)
                    startGame = new RelayCommand(() =>
                    {
                        if (this.startButtonVisibility == Visibility.Visible)
                            this.startButtonVisibility = Visibility.Collapsed;
                        else
                            this.startButtonVisibility = Visibility.Visible;

                        RaisePropertyChanged("StartButtonVisibility");
                    });
                return startGame;
            }
        }
    }
}
