using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    /// <summary>
    /// Enum used by each Note in its State machine
    /// Crucial in the handling of animations and user input
    /// </summary>
    public enum NoteState
    {
        Waiting,
        Playing,
        Hit,
        Missed,
        Paused,
        Resumed
    }
}
