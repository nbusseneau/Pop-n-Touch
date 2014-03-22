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
            foreach(string line in lines)
            {
                string[] infos = line.Split(' ');
                Length length = (Length)Enum.Parse(typeof(Length), infos[0], true);
                Accidental accidental = (Accidental)Enum.Parse(typeof(Accidental), infos[1], true);
                Height height = (Height)Enum.Parse(typeof(Height), infos[2], true);
                sheetMusic.Notes.Add(NoteFactory.Instance.GetNote(length, accidental, height));
                Console.WriteLine(length + " " + accidental + " " + height);
            }
            return sheetMusic;
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

            while (true) { }
        }*/
    }
}
