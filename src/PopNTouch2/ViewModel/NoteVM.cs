using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        public Note Note { get; set; }

        private NoteState state;
        public NoteState State
        {
            get { return state; }
            set 
            { 
                this.state = value;
                RaisePropertyChanged("State");
            }
        }

        public NoteVM(Note note)
        {
            this.Note = note;
            this.State = this.Note.State;
            this.Note.Tick += new Note.TickHandler(UpdateNoteState);
        }

        public void UpdateNoteState()
        {
            this.State = this.Note.State;
        }
    }
}
