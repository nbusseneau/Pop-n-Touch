using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class SheetBuilder
    {
        public Song Song { get; set; }
        public Difficulty Difficulty { get; set; }
        public Instrument Instrument { get; set; }
        public NoteFactory NoteFactory { get; set; }

        public SheetMusic buildSheet(Song song, Instrument instr, Difficulty diff)
        {
            SheetMusic sheetMusic = new SheetMusic();
            return sheetMusic;
        }
    }
}
