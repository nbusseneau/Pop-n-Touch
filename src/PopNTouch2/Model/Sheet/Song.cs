using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Song
    {
        public String Title { get; private set; }
        public String Author { get; private set; }
        public String Year { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public List<Instrument> Instruments { get; private set; }

        public Song(string title, string author, string year, Difficulty difficulty, List<Instrument> instruments)
        {
            this.Title = title;
            this.Author = author;
            this.Year = year;
            this.Difficulty = difficulty;
            this.Instruments = instruments;
        }
    }
}
