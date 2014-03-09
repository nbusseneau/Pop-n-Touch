using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Song
    {
        public String Name { get; private set; }
        public List<Instrument> Instruments { get; private set; }

        public Song(string n, List<Instrument> instrs)
        {
            this.Name = n;
            this.Instruments = instrs;
        }
    }
}
