using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;
using System.Collections.ObjectModel;
using System.Timers;

namespace PopNTouch2.ViewModel
{
    public class PlayerVM : ViewModelBase
    {
        public Player Player { get; set; }
        private MainWindowVM MainWindowVM { get; set; }
        private Timer CleaningTimer { get; set; }

        private Song loadedSong;

        // Interval, in milliseconds, between each sheet cleaning
        private const double CLEANING_INTERVAL = 10000;
        
        public PlayerVM(Player player, MainWindowVM mwvm)
        {
            this.Player = player;
            this.UpdateSong();
            this.Player.Tick += new Player.TickHandler(OnPlayerTick);
            this.MainWindowVM = mwvm;
        }

        /// <summary>
        /// Resets all PlayerVM info
        /// </summary>
        public void Reset()
        {
            this.BottomButtonsVisible = true;
            this.CanMove = true;
            this.ChoicesEnabled = false;
            this.ChoicesMade = false;
            this.Instruments = null;
            this.LockChecked = false;
            this.ReadyChecked = false;
            this.SheetMusicVM = new SheetMusicVM();
            this.ScoreVM = new ScoreVM();
            this.UpdateSong();
            RaisePropertyChanged("Combo");
            RaisePropertyChanged("Player");
        }

        /// <summary>
        /// Boolean property to set if position should be fixed
        /// </summary>
        private bool canMove = true;
        public bool CanMove
        {
            get { return canMove; }
            set 
            { 
                canMove = value;
                RaisePropertyChanged("CanMove");
            }
        }

        /// <summary>
        /// Boolean property to launch a flashing animation
        /// </summary>
        private bool flash = false;
        public bool Flash
        {
            get { return this.flash; }
            set
            {
                this.flash = value;
                RaisePropertyChanged("Flash");
            }
        }

        public void FlashAnimation()
        {
            this.Flash = true;
            this.Flash = false;
        }

        // Handles all of this Player's choices before starting the game : difficulty, instrument, ready
        #region Difficulty / Intrument choices

        /// <summary>
        /// Boolean property, are the choices enabled for the player to choose?
        /// Handles pretty much all of this region's visibility
        /// </summary>
        private bool choicesEnabled = true;
        public bool ChoicesEnabled
        {
            get { return this.choicesEnabled; }
            set
            {
                this.choicesEnabled = value;
                RaisePropertyChanged("ChoicesEnabled");
            }
        }

        /// <summary>
        /// Has the player picked a Difficulty?
        /// </summary>
        private bool diffPicked = false;

        /// <summary>
        /// Command, launched when player picks a difficulty
        /// </summary>
        private ICommand pickDifficulty;
        public ICommand PickDifficulty
        {
            get
            {
                if (this.pickDifficulty == null)
                    this.pickDifficulty = new RelayCommand<string>(arg =>
                    {
                        Difficulty difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), arg);
                        switch (difficulty)
                        {
                            case Difficulty.Beginner:
                                this.Player.Difficulty = Difficulty.Beginner;
                                break;
                            case Difficulty.Classic:
                                this.Player.Difficulty = Difficulty.Classic;
                                break;
                            case Difficulty.Expert:
                                this.Player.Difficulty = Difficulty.Expert;
                                break;
                            default:
                                throw new ArgumentException();
                        }
                        RaisePropertyChanged("Player");
                        this.diffPicked = true;
                        this.checkChoicesState();
                    }
                    );
                return this.pickDifficulty;
            }
        }

        /// <summary>
        /// Observable list of Instruments
        /// Watched in XAML
        /// </summary>
        private ObservableCollection<InstrumentVM> instruments;
        public IEnumerable<InstrumentVM> Instruments
        {
            get { return this.instruments; }
            set
            {
                this.instruments = (ObservableCollection<InstrumentVM>)value;
                RaisePropertyChanged("Instruments");
            }
        }

        /// <summary>
        /// Check if the selected song is different, and update accordingly
        /// </summary>
        public void UpdateSong()
        {
            Song currentSong = this.Player.CurrentGame.Song;
            if (this.loadedSong != currentSong)
            {
                this.loadedSong = currentSong;
                if (currentSong == null)
                {
                    return;
                }
                ObservableCollection<InstrumentVM> newList = new ObservableCollection<InstrumentVM>();
                foreach (Tuple<Instrument,Difficulty> instrument in currentSong.Sheets)
                {
                    newList.Add(new InstrumentVM(instrument, this));
                }
                this.Instruments = newList;
            }
        }

        /// <summary>
        /// Has the player picked an Instrument?
        /// </summary>
        private bool instruPicked = false;

        public void PickInstrument(Instrument instrument, Difficulty difficulty)
        {
            this.Player.Instrument = instrument;
            this.Player.InstrumentDifficulty = difficulty;
            RaisePropertyChanged("Player");
            this.instruPicked = true;
            foreach (InstrumentVM ivm in this.instruments)
            {
                ivm.Selected = false;
            }
            this.checkChoicesState();
        }

        /// <summary>
        /// Property, Has the player made all his choices?
        /// </summary>
        private bool choicesMade = false;
        public bool ChoicesMade
        {
            get { return this.choicesMade; }
            set 
            {
                this.choicesMade = value;
                RaisePropertyChanged("ChoicesMade");
            }
        }

        /// <summary>
        /// Checks whether or not the player has made all his choices
        /// </summary>
        private void checkChoicesState()
        {
            if (this.diffPicked && this.instruPicked)
            {
                this.ChoicesMade = true;
            }
            else
            {
                this.ChoicesMade = false;
            }
        }

        /// <summary>
        /// Property, two-way bound to the Ready check button
        /// </summary>
        private bool readyChecked = false;
        public bool ReadyChecked
        {
            get { return this.readyChecked; }
            set
            {
                this.readyChecked = value;
                RaisePropertyChanged("ReadyChecked");
            }
        }

        /// <summary>
        /// Command, launched when the player clicks the Ready button (in either state)
        /// </summary>
        private ICommand clickReady;
        public ICommand ClickReady
        {
            get
            {
                if (this.clickReady == null)
                    this.clickReady = new RelayCommand( () =>
                    {
                        if (this.readyChecked) {
                            if (this.Player.Difficulty == Difficulty.Beginner)
                            {
                                this.SheetMusicVM.easyMode = true;
                            }
                            this.Player.IMReady();
                        }
                        else
                        {
                            this.Player.NotReadyAnymore();
                            this.checkChoicesState();
                        }
                        RaisePropertyChanged("Player");
                    }
                    );
                return this.clickReady;
            }
        }
        #endregion

        #region SheetMusic & Play

        private SheetMusicVM sheetMusicVM = new SheetMusicVM();
        public SheetMusicVM SheetMusicVM
        {
            get { return this.sheetMusicVM; }
            set
            {
                this.sheetMusicVM = value;
                RaisePropertyChanged("SheetMusicVM");
            }
        }

        /// <summary>
        /// Command, launched when the player clicks one of the Note buttons
        /// Command Parameter is note pressed's height
        /// </summary>
        private ICommand clickNoteButton;
        public ICommand ClickNoteButton
        {
            get
            {
                if (this.clickNoteButton == null)
                    this.clickNoteButton = new RelayCommand<string>(arg =>
                    {
                        Height height = (Height)Enum.Parse(typeof(Height), arg);
                        this.PlayNote(height);
                    }
                    );
                return this.clickNoteButton;
            }
        }

        /// <summary>
        /// Sets correct current sheet in SheetMusicVM
        /// Sets and starts CleaningTimer
        /// </summary>
        public void PrepareSheet()
        {
            this.SheetMusicVM.Sheet = this.Player.SheetMusic;
            this.SheetMusicVM.Visibility = true;
            //this.CleaningTimer = new Timer(CLEANING_INTERVAL);
            //this.CleaningTimer.AutoReset = true;
            //this.CleaningTimer.Elapsed += new ElapsedEventHandler((source, e) => { this.SheetMusic.CleanNotes(this.Player.Stopwatch); });
            //this.CleaningTimer.Start();
        }

        /// <summary>
        /// Fired for each Player.Tick event
        /// Adds a Note to be played in SheetMusic
        /// </summary>
        /// <param name="p"></param>
        /// <param name="nt"></param>
        public void OnPlayerTick(Player p, Player.NoteTicked nt)
        {
            this.SheetMusicVM.AddNote(nt.Note);
            RaisePropertyChanged("SheetMusic");
        }

        /// <summary>
        /// Knowing that the user pressed button height, computes which Note is supposed to be pressed and compares
        /// </summary>
        /// <param name="height"></param>
        public void PlayNote(Height height)
        {
            this.Player.Stopwatch.Stop();
            double elapsedTime = this.Player.Stopwatch.ElapsedMilliseconds;
            this.Player.Stopwatch.Start();

            List<Tuple<double, double, Note>> playingNotes = this.Player.SheetMusic.Notes.FindAll(t => t.Item3.State == NoteState.Playing && t.Item3.Height == height);
            if (playingNotes.Count == 0)
            {
                this.SheetMusicVM.DisplayNoteFailed();
                this.Player.Combo = 0;
                RaisePropertyChanged("Combo");
                return;
            }
            
            double bestTiming = Math.Abs(playingNotes[0].Item2 - elapsedTime);
            Note bestNote = playingNotes[0].Item3;
            for (int i = 1; i < playingNotes.Count; i++ )
            {
                double timing = Math.Abs(playingNotes[i].Item2 - elapsedTime);
                if (timing < bestTiming)
                {
                    bestTiming = timing;
                    bestNote = playingNotes[i].Item3;
                }
            }

            if (bestTiming > Player.TIMING_TOLERANCE)
            {
                this.SheetMusicVM.DisplayNoteFailed();
                this.Player.Combo = 0;
                RaisePropertyChanged("Combo");
                return;
            }

            this.Player.NoteScored(bestTiming / 1000);
            this.SheetMusicVM.DisplayNoteScored();
            bestNote.Hit();
            this.SheetMusicVM.UpdateNoteState(bestNote);
            this.Player.ScoreCombo();
            this.ScoreVM.MaxCombo = this.Player.MaxCombo;
            this.ScoreVM.Precision = (int) Math.Floor((double) 100 * this.Player.Score/this.SheetMusicVM.Sheet.GetMaxScore());

            RaisePropertyChanged("Combo");
        }
        
        public void Pause()
        {
            foreach (Height h in Enum.GetValues(typeof(Height)))
            {
                foreach(NoteVM nvm in this.SheetMusicVM.GetHeightCollection(h)) 
                {
                    nvm.UpdateNoteState();
                }
            }
        }

        public void Resume()
        {
            this.Pause();
        }

        #endregion

        // Bottom buttons gestion (sup. & lock)
        #region Bottom buttons
        private bool bottomButtonsVisible = true;
        public bool BottomButtonsVisible
        {
            get { return this.bottomButtonsVisible; }
            set
            {
                this.bottomButtonsVisible = value;
                RaisePropertyChanged("BottomButtonsVisible");
                RaisePropertyChanged("PlayButtonsVisible");
            }
        }
        public bool PlayButtonsVisible
        {
            get { return !this.bottomButtonsVisible; }
        }

        /// <summary>
        /// Command, launched when the player clicks the Suppress button to suppress itself
        /// </summary>
        private ICommand removal;
        public ICommand Removal
        {
            get
            {
                if (this.removal == null)
                    this.removal = new RelayCommand(() =>
                    {
                        MainWindowVM.RemovePlayerVM(this);
                    }
                    );
                return this.removal;
            }
        }

        /// <summary>
        /// Property, two-way bound to the Lock button
        /// </summary>
        private bool lockChecked = false;
        public bool LockChecked
        {
            get { return this.lockChecked; }
            set
            {
                this.lockChecked = value;
                RaisePropertyChanged("LockChecked");
            }
        }

        /// <summary>
        /// Command, launched when the player clicks the Lock button (in either state)
        /// </summary>
        private ICommand clickLock;
        public ICommand ClickLock
        {
            get
            {
                if (this.clickLock == null)
                    this.clickLock = new RelayCommand(() =>
                    {
                        if (this.LockChecked)
                        {
                            this.CanMove = false;
                        }
                        else
                        {
                            this.CanMove = true;
                        }
                        RaisePropertyChanged("ClickLock");
                    }
                    );
                return this.clickLock;
            }
        }

        #endregion

        // Score
        #region Score
        public int Combo
        {
            get { return this.Player.Combo; }
        }

        private ScoreVM scoreVM = new ScoreVM();
        public ScoreVM ScoreVM
        {
            get { return this.scoreVM; }
            set
            {
                this.scoreVM = value;
                RaisePropertyChanged("ScoreVM");
            }
        }

        /// <summary>
        /// Display the score screen
        /// </summary>
        public void DisplayScore()
        {
            this.SheetMusicVM.Visibility = false;
            this.ScoreVM.ScoreVisibility = true;
            this.ScoreVM.Score = this.Player.Score;
        }

        #endregion
    }
}
