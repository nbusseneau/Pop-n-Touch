using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.ViewModel
{
    public class InstrumentVM : ViewModelBase
    {
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

        public InstrumentVM(Instrument instrument)
        {
            this.Instrument = instrument;
        }
    }
}
