using PopNTouch2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
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

        ICommand addPlayer;
        public ICommand AddPlayer
        {
            get
            {
                if (addPlayer == null)
                    addPlayer = new RelayCommand(() =>
                    {
                        Player player = new Player();
                        PlayerVM playerVM = new PlayerVM();
                        playerVM.Player = player;
                        GameMaster.Instance.NewPlayer(player);
                        this.players.Add(playerVM);
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

        ICommand playSong;
        public ICommand PlaySong
        {
            get
            {
                if (playSong == null)
                    playSong = new RelayCommand(() =>
                    {
                        // this.MainMenu.IsReady = false;
                        foreach (PlayerVM pvm in this.players)
                            pvm.ChoicesEnabled = true;
                    });
                return this.playSong;
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
