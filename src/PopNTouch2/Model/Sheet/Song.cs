using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Song
    {
        private string name;
        private List<Instrument> instruments;

        public String Name
        {
            get
            {
                return name;
            }
        }

        public List<Instrument> Instruments
        {
            get
            {
                return instruments;
            }
        }

        public Song(string n, List<Instrument> instrs)
        {
            name = n;
            instruments = instrs;
        }
    }
}
