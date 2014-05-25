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
using System.Timers;

namespace PopNTouch2.ViewModel
{
    /// <summary>
    /// Main class for display, handles most interactions like adding a player, starting / pausing a song, displaying MainMenu...
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        /// <summary>
        /// Create a new MainWindowVM
        /// </summary>
        public MainWindowVM()
        {
            this.mainMenu = new MainMenuVM(this);
        }

        // Start screen Behaviour, Properties and On Click Bindings
        #region Start screen

        /// <summary>
        /// Is Start screen visible?
        /// </summary>
        private bool startScreenVisible = true;
        public bool StartScreenVisible
        {
            get { return this.startScreenVisible; }
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
                        this.startScreenVisible = false;
                        RaisePropertyChanged("StartScreenVisible");

                        foreach(Song s in GameMaster.Instance.Songs)
                            this.MainMenu.AddSong(new SongVM(s, this.MainMenu));

                        this.mainMenu.IsVisible = true;
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
        /// Visibility of PlaySong, AddAPlayer and ReturnToMenu buttons
        /// </summary>
        private bool playSongButtonsVisible = false;
        public bool PlaySongButtonsVisible
        {
            get { return this.playSongButtonsVisible; }
            set
            {
                this.playSongButtonsVisible = value;
                RaisePropertyChanged("PlaySongButtonsVisible");
            }
        }

        /// <summary>
        /// Command launched when the "AddAPlayer" button is pressed
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
                        if (this.players.Count < 4) // FIXME : should be handled by RelayCommand.CanExecute (can't figure out how)
                        {
                            Player player = new Player();
                            GameMaster.Instance.NewPlayer(player);
                            PlayerVM playerVM = new PlayerVM(player, this);
                            this.players.Add(playerVM);
                            RaisePropertyChanged("Players");
                        }
                    });
                return this.addPlayer;
            }
        }

        /// <summary>
        /// Command launched when the "ReturnToMenu" button is pressed
        /// Removes all players from the game
        /// </summary>
        ICommand returnToMenu;
        public ICommand ReturnToMenu
        {
            get
            {
                if (returnToMenu == null)
                    returnToMenu = new RelayCommand(() =>
                    {
                        GameMaster.Instance.Game.Song = null;
                        foreach (Player player in GameMaster.Instance.Players)
                        {
                            player.Reset();
                        }
                        foreach (PlayerVM pvm in this.players)
                        {
                            pvm.Reset();
                        }
                        this.MainMenu.IsReady = false;
                        this.MainMenu.IsVisible = true;
                        this.PlaySongButtonsVisible = false;
                        
                    });
                return this.returnToMenu;
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
                        bool allReady = true;
                        foreach (PlayerVM pvm in this.players)
                        {
                            if (!pvm.ReadyChecked)
                            {
                                pvm.FlashAnimation();
                                allReady = false;
                            }
                        }

                        if (allReady && players.Count > 0)
                        {
                            this.DisablePlayerChoices();
                            foreach (PlayerVM pvm in this.players)
                            {
                                pvm.BottomButtonsVisible = false;
                                pvm.ScoreVM.ScoreVisibility = false;
                            }
                            this.PlaySongButtonsVisible = false;
                            this.PauseVisibility = true;

                            if (!GameMaster.Instance.Game.IsPlaying)
                            {
                                GameMaster.Instance.Game.Launch(); //FIXME : Have a Countdown before launching
                                GameMaster.Instance.Game.MusicPlayback.MediaEnded += new EventHandler(ComputeEndGame);
                            }
                            else
                            {
                                GameMaster.Instance.Resume();
                                foreach (PlayerVM pvm in this.Players)
                                {
                                    pvm.Resume();
                                }
                            }
                        }
                    });
                return this.playSong;
            }
        }

        /// <summary>
        /// Visibility of Pause
        /// </summary>
        private bool pauseVisibility = false;
        public bool PauseVisibility
        {
            get { return this.pauseVisibility; }
            set
            {
                this.pauseVisibility = value;
                RaisePropertyChanged("PauseVisibility");
            }
        }

        /// <summary>
        /// Command launched when the "Pause" button is pressed
        /// Pauses
        /// </summary>
        ICommand pause;
        public ICommand Pause
        {
            get
            {
                if (pause == null)
                    pause = new RelayCommand(() =>
                    {
                        if (GameMaster.Instance.Game.MusicPlayback.Position > TimeSpan.Zero)
                        {
                            this.PauseVisibility = false;
                            this.PlaySongButtonsVisible = true;
                            foreach (PlayerVM pvm in Players)
                            {
                                pvm.BottomButtonsVisible = true;
                                pvm.CanMove = true;
                            }

                            GameMaster.Instance.Pause();
                            foreach (PlayerVM pvm in this.Players)
                            {
                                pvm.Pause();
                            }
                        }
                    });
                return this.pause;
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
                p.ChoicesEnabled = true;
                p.UpdateSong();
            }
        }

        /// <summary>
        /// Disables every players' difficulty and instrument choice
        /// </summary>
        public void DisablePlayerChoices()
        {
            foreach (PlayerVM p in this.players)
            {
                p.ChoicesEnabled = false;
                p.CanMove = false;
                p.PrepareSheet();
            }
        }

        /// <summary>
        /// Remove the player
        /// </summary>
        /// <param name="playerVM"></param>
        public void RemovePlayerVM(PlayerVM playerVM)
        {
            GameMaster.Instance.Players.Remove(playerVM.Player);
            this.players.Remove(playerVM);
        }
        #endregion

        /// <summary>
        /// Display the score of each players
        /// Display "replay" or whatever button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComputeEndGame(object sender, EventArgs e)
        {
            Timer timer = new Timer(2000);
            timer.AutoReset = false;
            timer.Elapsed += new ElapsedEventHandler((sender2, e2) =>
            {
                foreach (PlayerVM pvm in this.Players)
                {
                    pvm.DisplayScore();
                    pvm.BottomButtonsVisible = true;
                    pvm.CanMove = true;
                }
                PauseVisibility = false;
                PlaySongButtonsVisible = true;
            });
            timer.Start();
        }
    }
}
