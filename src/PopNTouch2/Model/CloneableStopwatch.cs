using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class CloneableStopwatch : Stopwatch
    {
        public CloneableStopwatch() : base() { }

        public CloneableStopwatch Clone()
        {
            return (CloneableStopwatch)this.MemberwiseClone();
        }
    }
}
