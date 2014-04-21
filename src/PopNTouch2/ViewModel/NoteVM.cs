using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        public Note Note { get; set; }

        public NoteVM(Note note)
        {
            this.Note = note;
        }

    }
}
