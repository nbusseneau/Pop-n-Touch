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
                            case Difficulty.Beginner :
                                this.Player.Difficulty = Difficulty.Beginner;
                                break;
                            case Difficulty.Classic :
                                this.Player.Difficulty = Difficulty.Classic;
                                break;
                            case Difficulty.Expert :
                                this.Player.Difficulty = Difficulty.Expert;
                                break;
                        }
                    }
                    );

                return this.pickDifficulty;
            }
        }

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
