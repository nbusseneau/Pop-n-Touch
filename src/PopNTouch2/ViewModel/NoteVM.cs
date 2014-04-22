using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PopNTouch2.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        public Note Note { get; set; }

        private Point center;

        public Point Center
        {
            get { return center; }
            set 
            {
                center = value;
                RaisePropertyChanged("Center");
            }
        }

        public NoteVM(Note note)
        {
            this.Note = note;
        }
    }
}
