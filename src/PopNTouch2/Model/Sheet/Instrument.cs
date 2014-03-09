using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Instrument
    {
        public string Name { get; private set; }

        public Instrument(string n)
        {
            this.Name = n;
        }
    }
}
