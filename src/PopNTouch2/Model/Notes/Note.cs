using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Note
    {
        public Length Length { get; private set; }
        public Accidental Accidental { get; private set; }
        public Height Height { get; private set; }

        public Note(Length l, Accidental a, Height h)
        {
            this.Length = l;
            this.Accidental = a;
            this.Height = h;
        }
    }
}
