using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    // Bonuses not implemented
    public class SheetBuilder
    {
        private string extension = ".sheet";

        public string SongsDirectory { get; private set; }

        public SheetBuilder(string songsDirectory)
        {
            this.SongsDirectory = songsDirectory;
        }

        public SheetMusic BuildSheet(Song song, Instrument instr, Difficulty diff)
        {
            SheetMusic sheetMusic = new SheetMusic();
            int bpm = song.Bpm;
            double millitick = 60.0 / bpm * 1000;
            
            // Find the file
            string fileName = instr.ToString() + "-" + diff.ToString() + this.extension;
            string filePath = Path.Combine(this.SongsDirectory, song.Title, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("No file found for that song, instrument and difficulty. " + filePath);
                return sheetMusic;
            }

            // Read the file and build sheet
            string[] lines = File.ReadAllLines(filePath);
            sheetMusic.TimeRest = millitick * this.LengthValue(Length.Whole);
            double time = 0;
            foreach(string line in lines)
            {
                string[] infos = line.Split(' ');
                Length length = (Length)Enum.Parse(typeof(Length), infos[0], true);
                Accidental accidental = (Accidental)Enum.Parse(typeof(Accidental), infos[1], true);
                Height height = (Height)Enum.Parse(typeof(Height), infos[2], true);
                sheetMusic.Notes.Add(new Tuple<double, Note>(time, NoteFactory.Instance.GetNote(length, accidental, height)));
                //Console.WriteLine(length + " " + accidental + " " + height + " " + time);
                time = millitick * LengthValue(length);
            }
            return sheetMusic;
        }

        public double LengthValue(Length length)
        {
            switch (length)
            {
                case Length.Eighth:
                    return 0.5;
                case Length.Quarter:
                    return 1.0;
                case Length.Half:
                    return 2.0;
                case Length.Whole:
                    return 4.0;
                default:
                    Console.WriteLine("No associated Length. Put LengthValue in SheetBuilder up to date.");
                    return 0.0;
            }
        }

        /*public static void Main(string[] args)
        {
            GameMaster gameMaster = GameMaster.Instance;
            List<Song>.Enumerator es = gameMaster.Songs.GetEnumerator();
            if (es.Current == null) es.MoveNext();
            gameMaster.SelectSong(es.Current);
            List<Player>.Enumerator ep = gameMaster.Players.GetEnumerator();
            if (ep.Current == null) ep.MoveNext();
            ep.Current.Instrument = Instrument.Guitar;
            ep.Current.Difficulty = Difficulty.Beginner;
            ep.Current.IMReady();
            ep.Current.ReadSheet();

            while (true) { }
        }*/
    }
}
