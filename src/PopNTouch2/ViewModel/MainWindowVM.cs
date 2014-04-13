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
        public MainWindowVM()
        {
            this.mainMenu = new MainMenuVM(this);
        }

        // StartButton Behaviour, Properties and On Click Bindings
        #region StartButton

        /// <summary>
        /// Is the animated Start button visible?
        /// May be changed to a boolean
        /// </summary>
        private Visibility startButtonVisibility = Visibility.Visible;
        public Visibility StartButtonVisibility
        {
            get { return this.startButtonVisibility; }
        }

        /// <summary>
        /// Command launched when start button is pressed
        /// Must handle basic instanciations
        /// </summary>
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

                        foreach(Song s in GameMaster.Instance.Songs)
                            this.MainMenu.songs.Add(new SongVM(s, this.MainMenu));
                        RaisePropertyChanged("Songs");

                        // FIXME Temporary
                        this.mainMenu.Visibility = Visibility.Visible;
                    });
                return this.startGame;
            }
        }

        #endregion

        // MainMenu 
        #region MainMenu

        /// <summary>
        /// MainMenu ViewModel reference
        /// </summary>
        private MainMenuVM mainMenu;
        public MainMenuVM MainMenu
        {
            get { return this.mainMenu; }
        }

        #endregion

        // PlaySong buttons 
        #region PlaySong

        /// <summary>
        /// Command launched when the "+" button is pressed
        /// Adds a player to the game
        /// </summary>
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
                        playerVM.ChoicesEnabled = true;
                        playerVM.Player = player;
                        GameMaster.Instance.NewPlayer(player);
                        playerVM.UpdateSong();
                        this.players.Add(playerVM);
                        RaisePropertyChanged("Players");

                    });
                return this.addPlayer;
            }
        }

        /// <summary>
        /// Command launched when the "x" button is pressed
        /// Removes all players from the game
        /// </summary>
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

        /// <summary>
        /// Command launched when the Play button is pressed
        /// </summary>
        ICommand playSong;
        public ICommand PlaySong
        {
            get
            {
                if (playSong == null)
                    playSong = new RelayCommand(() =>
                    {
                    });
                return this.playSong;
            }
        }

        #endregion

        // Players behaviour, listing and observation
        #region PlayerTabs

        /// <summary>
        /// Observable list of Player ViewModels
        /// Watched in XAML
        /// </summary>
        private ObservableCollection<PlayerVM> players = new ObservableCollection<PlayerVM>();
        public IEnumerable<PlayerVM> Players
        {
            get { return this.players; }
        }

        /// <summary>
        /// Updates every player's Song
        /// </summary>
        public void UpdatePlayers()
        {
            foreach (PlayerVM p in this.players)
            {
                p.UpdateSong();
            }
        }

        #endregion
    }
}
