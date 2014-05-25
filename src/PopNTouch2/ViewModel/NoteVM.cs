using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    /// <summary>
    /// Prepares a Note for displaying, keeps track of its Note state
    /// </summary>
    public class NoteVM : ViewModelBase
    {
        public Note Note { get; set; }

        /// <summary>
        /// Create a new NoteVM for Note note
        /// </summary>
        /// <param name="note">Note, encapsulated for display</param>
        public NoteVM(Note note)
        {
            this.Note = note;
            this.State = this.Note.State;
            this.Note.Tick += new Note.TickHandler(UpdateNoteState);
        }

        /// <summary>
        /// Copy of current Note state to launch animations
        /// </summary>
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

        /// <summary>
        /// Is this Note to be displayed for Easy Mode?
        /// </summary>
        private bool easyMode = false;
        public bool EasyMode
        {
            get { return this.easyMode; }
            set 
            { 
                this.easyMode = value;
                RaisePropertyChanged("EasyMode");
            }
        }

        /// <summary>
        /// Checks Note state and update self
        /// </summary>
        public void UpdateNoteState()
        {
            this.State = this.Note.State;
        }
    }
}
