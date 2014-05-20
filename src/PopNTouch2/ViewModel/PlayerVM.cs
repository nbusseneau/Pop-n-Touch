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

        private Song loadedSong;

        private Timer CleaningTimer { get; set; }

        // Interval, in milliseconds, between each sheet cleaning
        private const double CLEANING_INTERVAL = 10000;


        public PlayerVM(Player player)
        {
            this.Player = player;
            this.UpdateSong();
            this.Player.Tick += new Player.TickHandler(OnPlayerTick);
        }

        /// <summary>
        /// Boolean property to set if position should be fixed
        /// </summary>
        private bool isFixed = true;
        public bool IsFixed
        {
            get { return isFixed; }
            set 
            { 
                isFixed = value;
                RaisePropertyChanged("IsFixed");
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
                ObservableCollection<InstrumentVM> newList = new ObservableCollection<InstrumentVM>();
                foreach (Tuple<Instrument,Difficulty> instrument in currentSong.Sheets)
                {
                    newList.Add(new InstrumentVM(instrument, this));
                }
                this.Instruments = newList;
                this.loadedSong = currentSong;
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

        private SheetMusicVM sheetMusic = new SheetMusicVM();
        public SheetMusicVM SheetMusic
        {
            get { return this.sheetMusic; }
            set
            {
                this.sheetMusic = value;
                RaisePropertyChanged("SheetMusic");
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
            // Build the sheet should be done in the Model
            this.SheetMusic.Sheet = GameMaster.Instance.SheetBuilder.BuildSheet(this.loadedSong, this.Player.Instrument, this.Player.InstrumentDifficulty);
            this.Player.SheetMusic = this.SheetMusic.Sheet;
            this.SheetMusic.Visibility = true;

            this.CleaningTimer = new Timer(CLEANING_INTERVAL);
            this.CleaningTimer.AutoReset = true;
            this.CleaningTimer.Elapsed += new ElapsedEventHandler((source, e) => { this.SheetMusic.CleanNotes(this.Player.Stopwatch); });
            this.CleaningTimer.Start();
        }

        /// <summary>
        /// Fired for each Player.Tick event
        /// Adds a Note to be played in SheetMusic
        /// </summary>
        /// <param name="p"></param>
        /// <param name="nt"></param>
        public void OnPlayerTick(Player p, Player.NoteTicked nt)
        {
            this.SheetMusic.AddNote(nt.Note);
            RaisePropertyChanged("SheetMusic");
        }


        public void PlayNote(Height height)
        {
            // TODO
        }

        #endregion
        

        public ScoreVM ScoreVM
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
