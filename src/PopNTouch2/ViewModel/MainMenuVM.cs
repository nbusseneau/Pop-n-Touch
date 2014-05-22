using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PopNTouch2.ViewModel
{
    public class MainMenuVM : ViewModelBase
    {
        private const int MAX_DISPLAYED_SONGS = 5;

        private MainWindowVM MainWindow { get; set; }

        public MainMenuVM(MainWindowVM mainWindow)
        {
            this.MainWindow = mainWindow;
            this.Angles = new List<double>()
            {
                45.682,
                0,
                -35.513,
                -90,
                35.513
            };
            this.Margins = new List<Thickness>() 
            {
                new Thickness(1018,582,802,298),
                new Thickness(0,-1085,-380,0),
                new Thickness(0,-1390,-280,0),
                new Thickness(0,-1450,0,0),
                new Thickness(0,-1280,370,0),
            };
            this.ImageKeys = new List<String>();
            for (int i = 1; i <= 5; i++)
            {
                this.ImageKeys.Add("BrushImgScroll" + i);
            }
        }

        /// <summary>
        /// Is the MainMenu ready to get to next stage?
        /// </summary>
        private bool isReady = false;
        public bool IsReady
        {
            get { return this.isReady; }
            set 
            {
                this.isReady = value;
                RaisePropertyChanged("IsReady");
            }
        }

        /// <summary>
        /// Global MainMenu visibility
        /// Should maybe be changed to boolean
        /// </summary>
        private bool isVisible = false;
        public bool IsVisible
        {
            get { return this.isVisible; }
            set
            {
                this.isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        // Song Selection
        #region Song Selection

        // Song resources
        // Maybe these should be computed, but where?
        // Keeping them here to avoid code-behind
        private List<double> Angles { get; set; }
        private List<Thickness> Margins { get; set; }
        private List<String> ImageKeys { get; set; }

        /// <summary>
        /// Observable list of Songs
        /// Watched in XAML
        /// </summary>
        public ObservableCollection<SongVM> songs = new ObservableCollection<SongVM>();
        public IEnumerable<SongVM> Songs
        {
            get { return this.songs; }
        }

        /// <summary>
        /// Currently selected song
        /// </summary>
        private Song selectedSong;
        public Song SelectedSong
        {
            get
            {
                return this.selectedSong;
            }
            set
            {
                this.selectedSong = value;
                RaisePropertyChanged("SelectedSong");
            }
        }

        public void AddSong(SongVM songVM)
        {
            int param = this.songs.Count % MAX_DISPLAYED_SONGS;
            songVM.Angle = this.Angles[param];
            songVM.Margin = this.Margins[param];
            this.songs.Add(songVM);
            RaisePropertyChanged("Songs");
        }

        public void SelectSong (Song selectedSong)
        {
            this.SelectedSong = selectedSong;
            this.IsVisible = false;
            this.IsReady = true;
            this.MainWindow.PlaySongButtonsVisible = true;
            GameMaster.Instance.SelectSong(this.SelectedSong);
            this.MainWindow.UpdatePlayers();
        }

        #endregion
    }

}
