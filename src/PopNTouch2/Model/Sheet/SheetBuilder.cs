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

        /// <summary>
        /// Associate a note length with a double value
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static double LengthValue(Length length)
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

        /// <summary>
        /// Build the sheet of the player
        /// </summary>
        /// <param name="song">The song chosen to be played</param>
        /// <param name="instr">The instrument of the player</param>
        /// <param name="diff">The difficulty chosen by the player</param>
        /// <returns></returns>
        public SheetMusic BuildSheet(Song song, Instrument instr, Difficulty diff)
        {
            SheetMusic sheetMusic = new SheetMusic();
            
            // Find the file
            string fileName = instr.ToString() + "-" + diff.ToString() + this.extension;
            string filePath = Path.Combine(this.SongsDirectory, song.Title, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("No file found for that song, instrument and difficulty : " + filePath);
                return sheetMusic;
            }

            // Read the file and build sheet
            string[] lines = File.ReadAllLines(filePath);
            double bpm = 0;
            if (lines.Length != 0 && lines[0].Split(' ')[0] == "bpm")
            {
                bpm = Convert.ToDouble(lines[0].Split(' ')[1]);
            }
            else
            {
                Console.WriteLine("No bpm.");
                return sheetMusic;
            }
            double millitick = 60.0 / bpm * 1000;
            sheetMusic.FirstRest = millitick * SheetBuilder.LengthValue(Length.Whole);
            double time = 0;
            foreach(string line in lines)
            {
                string[] infos = line.Split(' ');
                // Bpm change
                if (infos[0].ToLowerInvariant() == "bpm")
                {
                    double newBpm = Convert.ToDouble(infos[1]);
                    millitick = 60.0 / newBpm * 1000;
                }
                // Classic note
                else
                {
                    Length length = (Length)Enum.Parse(typeof(Length), infos[0], true);
                    Accidental accidental = (Accidental)Enum.Parse(typeof(Accidental), infos[1], true);
                    Height height = (Height)Enum.Parse(typeof(Height), infos[2], true);
                    time += millitick * LengthValue(length);
                    sheetMusic.Notes.Add(new Tuple<double, Note>(time, NoteFactory.Instance.GetNote(length, accidental, height)));
                }
            }
            // Set maximum score
            sheetMusic.MaxScore = sheetMusic.Notes.Count * 10;
            return sheetMusic;
        }

        /*public static void Main()
        {
            GameMaster gm = GameMaster.Instance;
            Player player = new Player();
            gm.NewPlayer(player);
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument,Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Piano, Difficulty.Classic));
            Song newBorn = new Song("New Born", "Muse", "2001", sheets);
            gm.SelectSong(newBorn);
            player.Instrument = Instrument.Piano;
            player.Difficulty = Difficulty.Classic;
            player.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, player.Instrument, player.Difficulty);
            player.IMReady();
            gm.Ready();
            Player p2 = new Player();
            p2.Instrument = Instrument.Piano;
            p2.Difficulty = Difficulty.Classic;
            p2.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, player.Instrument, player.Difficulty);
            System.Threading.Thread.Sleep(3000);
            gm.Game.AddPlayerInGame(p2);

            while (true) { }
         * 
         * 
            player.Tick += delegate(Player sender, Player.NoteTicked nt)
            {
                Console.WriteLine(nt.Note.Height);
            };
        }*/
    }
}
