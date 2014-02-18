using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class NoteFactory
    {
        private readonly Lazy<Note>[] notes;

        // Max values of enums for note positionning in array
        private int maxAccidental = Enum.GetValues(typeof(Accidental)).Length + 1;
        private int maxHeight = Enum.GetValues(typeof(Height)).Length + 1;

        public Lazy<Note>[] Notes
        {
            get
            {
                return notes;
            }
        }

        public NoteFactory()
        {
            // Lazy instantiation of each possible note using all the values of the 3 enums
            foreach (var length in (Length[])Enum.GetValues(typeof(Length)))
            {
                foreach (var accidental in (Accidental[])Enum.GetValues(typeof(Accidental)))
                {
                    foreach (var height in (Height[])Enum.GetValues(typeof(Height)))
                    {
                        // Store the lazy note in its appropriate position in the array
                        notes[(int)length * this.maxAccidental * this.maxHeight +
                              (int)accidental * this.maxHeight +
                              (int)height
                             ] = new Lazy<Note>(() => new Note(length, accidental, height));
                    }
                }
            }
        }

        public Note getNote(Length length, Accidental accidental, Height height)
        {
            return notes[(int)length * this.maxAccidental * this.maxHeight +
                         (int)accidental * this.maxHeight +
                         (int)height].Value;
        }
    }
}