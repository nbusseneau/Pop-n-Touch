using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    /// <summary>
    /// Prepares Songs for their display in the MainMenuVM
    /// Bubbles their selection to MainMenuVM
    /// </summary>
    public class SongVM : ViewModelBase
    {
        public Song Song { get; set; }
        public MainMenuVM MenuVM { get; set; }

        /// <summary>
        /// Creates a new SongVM for the Song song, child of MainMenuVM menuVM
        /// </summary>
        /// <param name="song"></param>
        /// <param name="menuVM"></param>
        public SongVM(Song song, MainMenuVM menuVM)
        {
            this.Song = song;
            this.MenuVM = menuVM;
        }

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

        public int ImageIndex
        {
            get { return this.Song.Index % 5; }
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
