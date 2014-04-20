using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    public class SongVM : ViewModelBase
    {
        public Song Song { get; set; }
        public MainMenuVM MenuVM { get; set; }

        private double angle;
        public double Angle
        {
            get { return angle; }
            set 
            { 
                angle = value;
                RaisePropertyChanged("Angle");
            }
        }

        private Thickness margin;
        public Thickness Margin
        {
            get { return margin; }
            set 
            { 
                margin = value;
                RaisePropertyChanged("Margin");
            }
        }

        private String imageKey;
        public String ImageKey
        {
            get { return imageKey; }
            set 
            { 
                imageKey = value;
                RaisePropertyChanged("ImageKey");
            }
        }

        public SongVM(Song song, MainMenuVM menuVM)
        {
            this.Song = song;
            this.MenuVM = menuVM;
        }

        private ICommand selectSong;
        public ICommand SelectSong
        {
            get
            {
                if (this.selectSong == null)
                    this.selectSong = new RelayCommand(() =>
                    {
                        this.MenuVM.SelectSong(this.Song);
                    }
                    );

                return this.selectSong;
            }
        }
    }
}
