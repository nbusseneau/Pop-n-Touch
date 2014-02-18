using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Note
    {
        private Length length;
        private Accidental accidental;
        private Height height;

        public Length Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        public Accidental Accidental
        {
            get
            {
                return accidental;
            }
            set
            {
                accidental = value;
            }
        }

        public Height Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public Note(Length l, Accidental a, Height h)
        {
            this.Length = l;
            this.Accidental = a;
            this.Height = h;
        }
    }
}
