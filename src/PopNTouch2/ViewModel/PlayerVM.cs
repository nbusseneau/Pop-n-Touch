using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    public class PlayerVM : ViewModelBase
    {
        public Player Player { get; set; }

        // Handles all of this Player's choices before starting the game : difficulty, instrument, ready
        #region Difficulty / Intrument choices

        /// <summary>
        /// Boolean property, are the choices enabled for the player to choose?
        /// Handles pretty much all of this region's visibility
        /// </summary>
        private bool choicesEnabled = false;
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
        /// Has the player picked a Difficulty?
        /// </summary>
        private bool instruPicked = false;

        /// <summary>
        /// Command, launched when player picks an instrument
        /// </summary>
        private ICommand pickInstrument;
        public ICommand PickInstrument
        {
            get
            {
                if (this.pickInstrument == null)
                    this.pickInstrument = new RelayCommand<string>(arg =>
                    {
                        /*
                        Instrument instrument = (Instrument)Enum.Parse(typeof(Instrument), arg);
                        switch (instrument)
                        {
                            default:
                                throw new ArgumentException();
                        } */
                        RaisePropertyChanged("Player.Instrument");
                        this.instruPicked = true;
                        this.checkChoicesState();
                    }
                    );

                return this.pickInstrument;
            }
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
                this.ChoicesMade = true;
            else
                this.ChoicesMade = false;
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
                        if (this.readyChecked)
                            this.Player.IMReady();
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

        public SheetMusicVM SheetMusicVM
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
