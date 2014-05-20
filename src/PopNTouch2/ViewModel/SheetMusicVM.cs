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
    public class SheetMusicVM : ViewModelBase
    {
        public SheetMusic Sheet { get; set; }

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
                    break;
                case Height.Re:
                    this.reNotes.Add(nvm);
                    break;
                case Height.Mi:
                    this.miNotes.Add(nvm);
                    break;
                case Height.Fa:
                    this.faNotes.Add(nvm);
                    break;
                case Height.Sol:
                    this.solNotes.Add(nvm);
                    break;
                case Height.La:
                    this.laNotes.Add(nvm);
                    break;
                case Height.Si:
                    this.siNotes.Add(nvm);
                    break;
                case Height.Rest:
                    this.rests.Add(nvm);
                    break;
            }
        }

        /// <summary>
        /// Okay, bear with me here, this one is tricky, but pretty cool
        /// This function basically cleans every Collection of NoteVM contained in this class
        /// To do so, we get all our own fields (wow amaze such leet skillz)
        /// Then, we compare Notes' perfect timing and current play time, and suppress them accordingly
        /// </summary>
        public void CleanNotes(Stopwatch playerStopwatch)
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (FieldInfo f in fields)
            {
                if (f.FieldType == typeof(ObservableCollection<NoteVM>))
                {
                    ObservableCollection<NoteVM> collection = (ObservableCollection<NoteVM>)f.GetValue(this);
                    for (int i = collection.Count - 1; i >= 0; i-- )
                    {
                        double perfectTiming = this.Sheet.GetPerfectTiming(collection[i].Note); // We could keep the current Note index and speed up the search
                        playerStopwatch.Stop();
                        double elapsedTime = playerStopwatch.ElapsedMilliseconds;
                        playerStopwatch.Start();
                        if (elapsedTime > perfectTiming + 50000d)
                        {
                            collection.RemoveAt(i);
                        }
                    }
                }
                
            }
        }

        /* I'll just keep this here, example of Reflection with Properties
         * 
        public void RaiseAllCollectionChanged()
        {
            System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo p in properties)
            {
                if (p.PropertyType == typeof(IEnumerable<NoteVM>))
                {
                    RaisePropertyChanged(p.Name);
                }
            }
        }
         */

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
