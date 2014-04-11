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
        public int Bpm { get; set; }
        public int Index { get; private set; }

        public Song(string title, string author, string year, List<Tuple<Instrument, Difficulty>> sheets, int bpm, int index = 0)
        {
            this.Title = title;
            this.Author = author;
            this.Year = year;
            this.Sheets = sheets;
            this.Bpm = bpm;
            this.Index = index;
        }

        /// <summary>
        /// Get list of available instruments for this song
        /// </summary>
        /// <returns>List of Instruments</returns>
        public List<Instrument> GetInstruments()
        {
            List<Instrument> instruments = new List<Instrument>();
            foreach (Tuple<Instrument, Difficulty> pair in this.Sheets)
            {
                Instrument instrument = pair.Item1;
                if (!instruments.Contains(instrument))
                {
                    instruments.Add(pair.Item1);
                }
            }
            return instruments;
        }
    }
}