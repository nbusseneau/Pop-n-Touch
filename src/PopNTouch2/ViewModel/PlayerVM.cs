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

        /// <summary>
        /// Handles all of this Player's choices before starting the game : difficulty, instrument, ready
        /// </summary>
        #region Difficulty / Intrument choices

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

        private bool diffPicked = false;
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

        private bool instruPicked = false;
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

        private void checkChoicesState()
        {
            if (this.diffPicked && this.instruPicked)
                this.ChoicesMade = true;
            else
                this.ChoicesMade = false;
        }

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
                            this.diffPicked = false;
                            this.instruPicked = false;
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
