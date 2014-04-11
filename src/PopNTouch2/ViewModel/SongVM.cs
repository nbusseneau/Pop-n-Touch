using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    public class SongVM : ViewModelBase
    {
        public Song Song { get; set; }
        public MainMenuVM MenuVM { get; set; }

        public SongVM(Song song, MainMenuVM menuVM)
        {
            this.Song = song;
            this.MenuVM = menuVM;
        }
    }
}
