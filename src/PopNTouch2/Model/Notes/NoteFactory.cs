using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public sealed class NoteFactory
    {
        private static readonly NoteFactory instance = new NoteFactory();
        private readonly Lazy<Note>[] notes;

        // Max values of enums for note positionning in array and array instantiation
        private int maxLength = Enum.GetValues(typeof(Length)).Length;
        private int maxAccidental = Enum.GetValues(typeof(Accidental)).Length;
        private int maxHeight = Enum.GetValues(typeof(Height)).Length;

        public static NoteFactory Instance
        {
            get { return instance; }
        }

        public NoteFactory()
        {
            this.notes = new Lazy<Note>[this.maxLength * this.maxAccidental * this.maxHeight];

            // Lazy instantiation of each possible note using all the values of the 3 enums
            for (int length = 0; length < this.maxLength; length++)
            {
                for (int accidental = 0; accidental < this.maxAccidental; accidental++)
                {
                    for (int height = 0; height < this.maxHeight; height++)
                    {
                        // Casting back to enums
                        Length l = (Length)length;
                        Accidental a = (Accidental)accidental;
                        Height h = (Height)height;

                        // Store the lazy note in its appropriate position in the array
                        this.notes[length * this.maxAccidental * this.maxHeight +
                                   accidental * this.maxHeight +
                                   height
                                  ] = new Lazy<Note>(() => new Note(l, a, h));
                    }
                }
            }
        }

        public Note GetNote(Length length, Accidental accidental, Height height)
        {
            return this.notes[(int)length * this.maxAccidental * this.maxHeight +
                              (int)accidental * this.maxHeight +
                              (int)height].Value;
        }
    }
}