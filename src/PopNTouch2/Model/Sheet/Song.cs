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
        public List<Tuple<Instrument, Difficulty>> Sheets { get; private set; }
        public Uri File { get; private set; }
        public int Index { get; private set; }

        public Song(string title, string author, string year, List<Tuple<Instrument, Difficulty>> sheets, Uri file, int index = 0)
        {
            this.Title = title;
            this.Author = author;
            this.Year = year;
            this.Sheets = sheets;
            this.File = file;
            this.Index = index;
        }
    }
}
