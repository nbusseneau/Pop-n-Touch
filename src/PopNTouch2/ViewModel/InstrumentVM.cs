using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PopNTouch2.ViewModel
{
    public class InstrumentVM : ViewModelBase
    {

        public InstrumentVM(Instrument instrument, PlayerVM playerVM)
        {
            this.Instrument = instrument;
            this.PlayerVM = playerVM;
        }

        private Instrument instrument;
        public Instrument Instrument
        {
            get { return this.instrument; }
            set
            {
                this.instrument = value;
                RaisePropertyChanged("Instrument");
            }
        }

        private PlayerVM playerVM;
        public PlayerVM PlayerVM {
            get { return this.playerVM; }
            set
            {
                this.playerVM = value;
                RaisePropertyChanged("PlayerVM");
            }
        }

        private bool selected = false;
        public bool Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                this.selected = value;
                RaisePropertyChanged("Selected");
            }
        }

        /// <summary>
        /// Command, launched when player picks an instrument
        /// </summary>
        private ICommand pickInstrument;
        public ICommand PickInstrument
        {
            get
            {
                if (this.pickInstrument == null)
                    this.pickInstrument = new RelayCommand(()=>
                    {
                        this.PlayerVM.PickInstrument(this.Instrument);
                        this.Selected = true;
                    }
                    );
                return this.pickInstrument;
            }
        }
    }
}
