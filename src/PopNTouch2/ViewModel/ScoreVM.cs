using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopNTouch2.ViewModel
{
    public class ScoreVM : ViewModelBase
    {
        public ScoreVM()
        {
        }

        private int score = 0;
        public int Score
        {
            get { return this.score; }
            set
            {
                this.score = value;
                RaisePropertyChanged("Score");
            }
        }

        private int maxCombo;
        public int MaxCombo
        {
            get { return this.maxCombo; }
            set 
            { 
                this.maxCombo = value;
                RaisePropertyChanged("MaxCombo");
            }
        }

        private int precision = 0;
        public int Precision
        {
            get { return this.precision; }
            set 
            { 
                this.precision = value;
                RaisePropertyChanged("Precision");
            }
        }

        private bool scoreVisibility = false;
        public bool ScoreVisibility
        {
            get { return this.scoreVisibility; }
            set
            {
                this.scoreVisibility = value;
                RaisePropertyChanged("ScoreVisibility");
            }
        }
    }
}
