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
    /// <summary>
    /// Class used to handle the main rotating Menu displaying available Songs
    /// </summary>
    public class MainMenuVM : ViewModelBase
    {
        /// <summary>
        /// Maximum number of songs displayed at once
        /// </summary>
        private const int MAX_DISPLAYED_SONGS = 5;

        private MainWindowVM MainWindow { get; set; }

        /// <summary>
        /// Creates a new MainMenuVM
        /// </summary>
        /// <param name="mainWindow">MainWindowVM parent</param>
        public MainMenuVM(MainWindowVM mainWindow)
        {
            this.MainWindow = mainWindow;
            this.Angles = new List<double>()
            {
                 0,
                45,
                90,
                -45,
                -90,
            };
            this.Margins = new List<Thickness>() 
            {
                new Thickness(0,0,0,0),
                new Thickness(250,-100,0,0),
                new Thickness(350,-25,0,0),
                new Thickness(0,0,0,0),
                new Thickness(0,0,0,0),
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

        /// <summary>
        /// Add a new song to be displayed
        /// </summary>
        /// <param name="songVM">SongVM attached to the displayed song</param>
        public void AddSong(SongVM songVM)
        {
            int param = this.songs.Count % MAX_DISPLAYED_SONGS;
            songVM.Angle = this.Angles[param];
            songVM.Margin = this.Margins[param];
            this.songs.Add(songVM);
            RaisePropertyChanged("Songs");
        }

        /// <summary>
        /// Select a Song from thoses displayed
        /// </summary>
        /// <param name="selectedSong">Song, the selected Song provided by its SongVM</param>
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
