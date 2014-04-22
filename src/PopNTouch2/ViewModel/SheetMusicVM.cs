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

        public SheetMusicVM()
        {
        }

        /// <summary>
        /// Observable list of Notes
        /// Watched in XAML
        /// </summary>
        #region Notes ObservableCollections

        private ObservableCollection<NoteVM> doNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> DoNotes
        {
            get { return this.doNotes; }
            set
            {
                this.doNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("DoNotes");
            }
        }

        private ObservableCollection<NoteVM> reNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> ReNotes
        {
            get { return this.reNotes; }
            set
            {
                this.reNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("ReNotes");
            }
        }

        private ObservableCollection<NoteVM> miNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> MiNotes
        {
            get { return this.miNotes; }
            set
            {
                this.miNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("MiNotes");
            }
        }

        private ObservableCollection<NoteVM> faNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> FaNotes
        {
            get { return this.faNotes; }
            set
            {
                this.faNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("FaNotes");
            }
        }

        private ObservableCollection<NoteVM> solNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> SolNotes
        {
            get { return this.solNotes; }
            set
            {
                this.solNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("SolNotes");
            }
        }

        private ObservableCollection<NoteVM> laNotes= new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> LaNotes
        {
            get { return this.laNotes; }
            set
            {
                this.laNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("LaNotes");
            }
        }

        private ObservableCollection<NoteVM> siNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> SiNotes
        {
            get { return this.siNotes; }
            set
            {
                this.siNotes = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("SiNotes");
            }
        }

        private ObservableCollection<NoteVM> rests = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> Rests
        {
            get { return this.rests; }
            set
            {
                this.rests = (ObservableCollection<NoteVM>)value;
                RaisePropertyChanged("Rests");
            }
        }

        #endregion

        /// <summary>
        /// Adds Note note to collection
        /// </summary>
        /// <param name="note"></param>
        public void AddNote(Note note)
        {
            NoteVM nvm = new NoteVM(note);
            switch (note.Height)
            {
                case Height.Do :
                    this.doNotes.Add(nvm);
                    RaisePropertyChanged("DoNotes");
                    break;
                case Height.Re:
                    this.reNotes.Add(nvm);
                    RaisePropertyChanged("ReNotes");
                    break;
                case Height.Mi:
                    this.miNotes.Add(nvm);
                    RaisePropertyChanged("MiNotes");
                    break;
                case Height.Fa:
                    this.faNotes.Add(nvm);
                    RaisePropertyChanged("FaNotes");
                    break;
                case Height.Sol:
                    this.solNotes.Add(nvm);
                    RaisePropertyChanged("SolNotes");
                    break;
                case Height.La:
                    this.laNotes.Add(nvm);
                    RaisePropertyChanged("LaNotes");
                    break;
                case Height.Si:
                    this.siNotes.Add(nvm);
                    RaisePropertyChanged("SiNotes");
                    break;
                case Height.Rest:
                    this.rests.Add(nvm);
                    RaisePropertyChanged("Rests");
                    break;
            }
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
