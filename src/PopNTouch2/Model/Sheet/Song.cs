using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    /// <summary>
    /// Encapsulates all metadata and available sheets for a song found
    /// </summary>
    public class Song
    {
        public String Title { get; private set; }
        public String Author { get; private set; }
        public String Year { get; private set; }
        public List<Tuple<Instrument, Difficulty>> Sheets { get; private set; }
        public Uri File { get; private set; }
        public int Index { get; private set; }

        /// <summary>
        /// Creates a new Song with its metadata, sheet musics, file path and index
        /// </summary>
        /// <param name="title">string, song title</param>
        /// <param name="author">string, song author</param>
        /// <param name="year">string, song year</param>
        /// <param name="sheets">List of (Instrument, Difficulty) tuples, available sheet music for this song</param>
        /// <param name="file">Uri, file path</param>
        /// <param name="index">int, song indexing order, defaults to 0</param>
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
