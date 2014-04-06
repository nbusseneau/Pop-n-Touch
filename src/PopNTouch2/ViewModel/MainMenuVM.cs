﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;
using System.Windows;

namespace PopNTouch2.ViewModel
{
    public class MainMenuVM : ViewModelBase
    {
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
            set { this.visibility = value;
                  RaisePropertyChanged("Visibility"); }
        }

        // Song Selection
        #region Song Selection

        // FIXME : Temporary, will be a Song in the future
        private string selectedSong;
        public string SelectedSong
        {
            get
            {
                return this.selectedSong;
            }
        }

        ICommand selectSong;
        public ICommand SelectSong
        {
            get
            {
                if (this.selectSong == null)
                    this.selectSong = new RelayCommand<string>(songId =>
                    {
                        // TODO : Convert from string songId to Song
                        this.selectedSong = songId;
                        RaisePropertyChanged("SelectedSong");
                        // FIXME : Temporary
                        this.visibility = Visibility.Collapsed;
                        RaisePropertyChanged("Visibility");
                        this.isReady = true;
                        RaisePropertyChanged("IsReady");
                    }
                    );

                return this.selectSong;
            }
        }

        #endregion

    }

}
