using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PopNTouch2.Model;

namespace PopNTouch2.ViewModel
{
    public class MainMenuVM : ViewModelBase
    {
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
                    this.selectSong = new RelayCommand<string>( songId =>
                        {
                            // TODO : Convert from string songId to Song
                            this.selectedSong = songId;
                            RaisePropertyChanged("SelectedSong");
                        }
                    );

                return this.selectSong;
            }
        }
    }

}
