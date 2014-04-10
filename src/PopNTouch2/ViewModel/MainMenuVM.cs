using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;
using System.Windows;
using System.Collections.ObjectModel;

namespace PopNTouch2.ViewModel
{
    public class MainMenuVM : ViewModelBase
    {
        private MainWindowVM MainWindow { get; set; }

        public MainMenuVM(MainWindowVM mainWindow)
        {
            this.MainWindow = mainWindow;
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
        private Visibility visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get { return this.visibility; }
            set
            {
                this.visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        // Song Selection
        #region Song Selection

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
        /// Command launched when a player clicks on any of the main menu song items
        /// Takes a integer Song Index as a parameter
        /// Sets the Selected Song
        /// </summary>
        ICommand selectSong;
        public ICommand SelectSong
        {
            /* 
            get
            {
                if (this.selectSong == null)
                    this.selectSong = new RelayCommand<int>(songId =>
                    {
                        this.SelectedSong = GameMaster.Instance.Songs[songId];
                        // FIXME : Temporary
                        this.Visibility = Visibility.Collapsed;
                        this.IsReady = true;
                    }
                    );

                return this.selectSong;
            }
            */

            // FIXME : temporary, to get past the CommandParameter Error
            // Use the above commented version once fixed
            get
            {
                if (this.selectSong == null)
                    this.selectSong = new RelayCommand(() =>
                    {
                        this.SelectedSong = GameMaster.Instance.Songs[0];
                        // FIXME : Temporary
                        this.Visibility = Visibility.Collapsed;
                        this.IsReady = true;
                        GameMaster.Instance.SelectSong(this.SelectedSong);
                        this.MainWindow.UpdatePlayers();
                    }
                    );

                return this.selectSong;
            }
        }

        #endregion

    }

}
