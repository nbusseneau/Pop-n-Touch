using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2
{
    public interface INote
    {
        Length Length
        {
            get;
            set;
        }

        Accidental Accidental
        {
            get;
            set;
        }

        Height Height
        {
            get;
            set;
        }
    }
}
