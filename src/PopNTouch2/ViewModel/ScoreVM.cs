using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopNTouch2.ViewModel
{
    public class ScoreVM : ViewModelBase
    {
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

        public ScoreVM()
        {
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
