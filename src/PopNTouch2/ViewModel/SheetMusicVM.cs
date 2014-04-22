using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopNTouch2.ViewModel
{
    public class SheetMusicVM : ViewModelBase
    {
        public SheetMusic Sheet { get; set; }

        /// <summary>
        /// Y axis distances to place each note correctly
        /// </summary>
        private Dictionary<Height, double> NoteOffsets { get; set; }

        public SheetMusicVM()
        {
            double currentOffset = 150;
            double offset = 20;
            this.NoteOffsets = new Dictionary<Height, double>()
            {
                {Height.Do, currentOffset},
                {Height.Rest, currentOffset / 2},
            };

            foreach (string s in Enum.GetNames(typeof(Height)))
            {
                Height h = (Height) Enum.Parse(typeof(Height), s);
                if (h.Equals(Height.Do) || h.Equals(Height.Rest))
                {
                    continue;
                }
                currentOffset -= offset;
                this.NoteOffsets.Add(h, currentOffset);
            }
        }

        /// <summary>
        /// Observable list of Notes
        /// Watched in XAML
        /// </summary>
        private ObservableCollection<NoteVM> notes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> Notes
        {
            get { return this.notes; }
            set
            {
                this.notes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("Instruments");
            }
        }

        /// <summary>
        /// Adds Note note to collection
        /// </summary>
        /// <param name="note"></param>
        public void AddNote(Note note)
        {
            NoteVM nvm = new NoteVM(note);
            nvm.SetStartCenter(this.NoteOffsets[note.Height]);
            this.notes.Add(nvm);
            RaisePropertyChanged("Notes");
        }

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
