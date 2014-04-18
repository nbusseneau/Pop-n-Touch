using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopNTouch2.ViewModel
{
    public class SheetMusicVM : ViewModelBase
    {
        public SheetMusic Sheet { get; set; }

        private bool visibility = false;
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public List<BonusVM> BonusVM
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<NoteButtonVM> NoteButtonVM
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
