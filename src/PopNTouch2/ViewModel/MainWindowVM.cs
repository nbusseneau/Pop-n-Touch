using PopNTouch2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private MainWindow view = new MainWindow();

        public MainWindowVM()
        {
            this.PlaySongVisibility = Visibility.Collapsed;
            Binding menuReadyBinding = new Binding("IsReady");
            menuReadyBinding.Source = this.mainMenu;
            BindingOperations.SetBinding(this.view, PlaySongVisibilityProperty, menuReadyBinding);
        }

        /// <summary>
        /// StartButton Behaviour, Properties and On Click Bindings
        /// </summary>
        #region StartButton

        private Visibility startButtonVisibility = Visibility.Visible;
        public Visibility StartButtonVisibility
        {
            get { return this.startButtonVisibility; }
        }

        ICommand startGame;
        public ICommand StartGame
        {
            get
            {
                if (startGame == null)
                    startGame = new RelayCommand(() =>
                    {
                        this.startButtonVisibility = Visibility.Collapsed;
                        RaisePropertyChanged("StartButtonVisibility");

                        // FIXME Temporary
                        this.mainMenu.Visibility = Visibility.Visible;
                    });
                return this.startGame;
            }
        }

        #endregion

        /// <summary>
        /// MainMenu 
        /// </summary>
        #region MainMenu

        private MainMenuVM mainMenu = new MainMenuVM();
        public MainMenuVM MainMenu
        {
            get { return this.mainMenu; }
        }

        #endregion

        /// <summary>
        /// PlaySong buttons 
        /// </summary>
        #region PlaySong

        public Visibility PlaySongVisibility
        {
            get { return (Visibility) this.view.GetValue(PlaySongVisibilityProperty); }
            set
            {
                if (value != null)
                    this.view.SetValue(PlaySongVisibilityProperty, value);
                else
                {
                    Visibility playSongVisiblity = (this.mainMenu.IsReady) ? Visibility.Visible : Visibility.Collapsed;
                    this.view.SetValue(PlaySongVisibilityProperty, playSongVisiblity);
                }
                RaisePropertyChanged("PlaySongVisibility");
            }
        }

        public static readonly DependencyProperty PlaySongVisibilityProperty = DependencyProperty.Register("PlaySongVisibility", typeof(Visibility), typeof(MainWindow));


        ICommand addPlayer;
        public ICommand AddPlayer
        {
            get
            {
                if (addPlayer == null)
                    addPlayer = new RelayCommand(() =>
                    {
                        this.players.Add(new PlayerVM());
                        RaisePropertyChanged("Players");

                    });
                return this.addPlayer;
            }
        }

        ICommand eraseAll;
        public ICommand EraseAll
        {
            get
            {
                if (eraseAll == null)
                    eraseAll = new RelayCommand(() =>
                    {
                        for (int i = this.players.Count - 1; i >= 0; i--)
                            this.players.RemoveAt(i);
                        RaisePropertyChanged("Players");

                    });
                return this.eraseAll;
            }
        }

        #endregion

        /// <summary>
        /// Players behaviour, listing and observation
        /// </summary>
        #region PlayerTabs

        private ObservableCollection<PlayerVM> players = new ObservableCollection<PlayerVM>();

        public IEnumerable<PlayerVM> Players
        {
            get { return this.players; }
        }

        #endregion
    }
}
