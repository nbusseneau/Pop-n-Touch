using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PopNTouch2.ViewModel
{
    /// <summary>
    /// Holder of many ObservableCollections, to store and display dynamically every NoteVM created from a Sheet
    /// </summary>
    public class SheetMusicVM : ViewModelBase
    {
        public SheetMusic Sheet { get; set; }

        public bool easyMode = false;

        public SheetMusicVM()
        {
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

        /// <summary>
        /// Observable list of Notes
        /// Watched in XAML
        /// </summary>
        #region Notes ObservableCollections

        protected ObservableCollection<NoteVM> doNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> DoNotes
        {
            get { return this.doNotes; }
        }

        protected ObservableCollection<NoteVM> reNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> ReNotes
        {
            get { return this.reNotes; }
        }

        protected ObservableCollection<NoteVM> miNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> MiNotes
        {
            get { return this.miNotes; }
        }

        protected ObservableCollection<NoteVM> faNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> FaNotes
        {
            get { return this.faNotes; }
        }

        protected ObservableCollection<NoteVM> solNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> SolNotes
        {
            get { return this.solNotes; }
        }

        protected ObservableCollection<NoteVM> laNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> LaNotes
        {
            get { return this.laNotes; }
        }

        protected ObservableCollection<NoteVM> siNotes = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> SiNotes
        {
            get { return this.siNotes; }
        }

        protected ObservableCollection<NoteVM> rests = new AsyncObservableCollection<NoteVM>();
        public IEnumerable<NoteVM> Rests
        {
            get { return this.rests; }
        }

        #endregion

        /// <summary>
        /// Gets the ObservableCollection containing notes of Height height
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public ObservableCollection<NoteVM> GetHeightCollection(Height height)
        {
            switch (height)
            {
                case Height.Do:
                    return this.doNotes;
                case Height.Re:
                    return this.reNotes;
                case Height.Mi:
                    return this.miNotes;
                case Height.Fa:
                    return this.faNotes;
                case Height.Sol:
                    return this.solNotes;
                case Height.La:
                    return this.laNotes;
                case Height.Si:
                    return this.siNotes;
                case Height.Rest:
                    return this.rests;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Adds Note note to collection
        /// </summary>
        /// <param name="note"></param>
        public void AddNote(Note note)
        {
            NoteVM nvm = new NoteVM(note);
            if (this.easyMode)
            {
                nvm.EasyMode = true;
            }
            this.GetHeightCollection(note.Height).Add(nvm);
        }

        /// <summary>
        /// Update State of Note note from collections
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNoteState(Note note)
        {
            ObservableCollection<NoteVM> collection = this.GetHeightCollection(note.Height);
            collection.First(nvm => nvm.Note.Equals(note)).UpdateNoteState();
        }

        /// <summary>
        /// Boolean property to launch a note missed animation
        /// </summary>
        private bool missedNoteAnim = false;
        public bool MissedNoteAnim
        {
            get { return this.missedNoteAnim; }
            set
            {
                this.missedNoteAnim = value;
                RaisePropertyChanged("MissedNoteAnim");
            }
        }

        /// <summary>
        /// Triggers animation for a missed note
        /// </summary>
        public void DisplayNoteFailed()
        {
            this.MissedNoteAnim = true;
            this.MissedNoteAnim = false;
        }

        /// <summary>
        /// Boolean property to launch a note scored animation
        /// </summary>
        private bool scoredNoteAnim = false;
        public bool ScoredNoteAnim
        {
            get { return this.scoredNoteAnim; }
            set
            {
                this.scoredNoteAnim = value;
                RaisePropertyChanged("ScoredNoteAnim");
            }
        }

        /// <summary>
        /// Triggers animation for a hit note
        /// </summary>
        public void DisplayNoteScored()
        {
            this.ScoredNoteAnim = true;
            this.ScoredNoteAnim = false;
        }

        //public List<BonusVM> BonusVM
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}
    }
}
