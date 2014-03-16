using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class GameMaster
    {
        /*private List<Player> players;
        private Game game;
        private Boolean upToDateGame;
        private SheetBuilder sheetBuilder;*/
        private String SongsDirectory = @"Resources\Songs\";

        public List<Song> Songs { get; private set; }
        public List<Player> Players { get; set; }
        public Game Game { get; set; }
        public Boolean UpToDateGame { get; set; }
        public SheetBuilder SheetBuilder { get; set; }

        public GameMaster()
        {
            this.Songs = this.LoadSongs();
            this.Players = new List<Player>();
            this.UpToDateGame = false;
            this.SheetBuilder = new SheetBuilder();
        }

        private List<Song> LoadSongs()
        {
            List<Song> songs = new List<Song>();
            string[] dirs = Directory.GetDirectories(this.SongsDirectory);
            foreach (string songDirectory in dirs)
            {
                string title = null, author = null, year = null;
                Difficulty difficulty = Difficulty.Beginner; // Dummy initialization just to avoid a warning
                string metadata = Path.Combine(songDirectory, "meta.data");

                // Check for metadata
                if (File.Exists(metadata)) {
                    string[] lines = File.ReadAllLines(metadata);
                    if (lines.Length == 4)
                    {
                        title = lines[0];
                        author = lines[1];
                        year = lines[2];
                        difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), lines[3], true);
                    }
                }

                // If metadata was found, we can add instruments
                if (title != null)
                {
                    List<Instrument> instruments = new List<Instrument>();
                    string[] sheets = Directory.GetFiles(songDirectory, "*.sheet");
                    foreach (string sheet in sheets)
                    {
                        string instr = Path.GetFileNameWithoutExtension(sheet).Split('-')[0];
                        Instrument instrument = (Instrument)Enum.Parse(typeof(Instrument), instr, true);
                        if (!instruments.Contains(instrument))
                        {
                            instruments.Add(instrument);
                        }
                    }

                    songs.Add(new Song(title, author, year, difficulty, instruments));
                }
            }

            return songs;
        }

        public void SelectSong(Song song)
        {
            this.Game = new Game(song);
            this.UpToDateGame = true;
            if (!this.Players.Any())
            {
                this.Players.Add(new Player(this));
            }
            foreach (Player player in this.Players)
            {
                player.InformNewGame();
            }
        }

        // Attribute ok ? See later
        public void NewPlayer()
        {
            Player player = new Player(this);
            this.Players.Add(player);
            if (this.UpToDateGame)
                player.InformNewGame();
        }

        // Called by a player > at least one player existing and ready
        public void Ready()
        {
            Boolean everyoneReady = true;
            foreach (Player player in this.Players)
            {
                everyoneReady &= player.Ready;
            }
            if (everyoneReady && !this.Game.IsPlaying)
                this.Game.Launch();
            else if (everyoneReady && this.Game.IsPlaying)
                ; // Place the sheet reader to the right part of the sheet then play
        }
    }
}