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
        private SheetMusicVM SheetMusicVM { get; set; }

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

        public NoteVM(Note note, SheetMusicVM smvm)
        {
            this.Note = note;
            this.SheetMusicVM = smvm;
        }


        private ICommand die;
        public ICommand Die
        {
            get
            {
                if (this.die == null)
                    this.die = new RelayCommand(() =>
                    {
                        this.SheetMusicVM.RemoveNote(this);
                    }
                    );
                return this.die;
            }
        }
    }
}
