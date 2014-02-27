using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Instrument
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Instrument(string n) { name = n; }
    }
}
