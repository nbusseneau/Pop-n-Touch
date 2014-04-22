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
        private const double CONTAINER_WIDTH = 580;

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

        public void SetStartCenter(double topOffset)
        {
            this.Center = new Point(CONTAINER_WIDTH - 30, topOffset);
        }
    }
}
