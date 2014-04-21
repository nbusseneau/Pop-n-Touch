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
            this.notes.Add(new NoteVM(note));
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
