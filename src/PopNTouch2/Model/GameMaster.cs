using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public sealed class GameMaster
    {
        private static readonly GameMaster instance = new GameMaster();
        private string SongsDirectory = @"Resources\Songs\";

        public static GameMaster Instance
        {
            get { return instance; }
        }
        // Big MVVM nope. Don't do that at home kids !
        public static int TIMETOPLAY = 2500;
        public static int TIMEBEFORERESUME = 1000;
        public List<Song> Songs { get; private set; }
        public List<Player> Players { get; private set; }
        public Game Game { get; private set; }
        public Boolean SongSelected { get; private set; }
        public SheetBuilder SheetBuilder { get; private set; }

        private GameMaster()
        {
            this.Songs = this.LoadSongs();
            this.Players = new List<Player>();
            this.SongSelected = false;
            this.SheetBuilder = new SheetBuilder(this.SongsDirectory);
        }

        /// <summary>
        /// Find and stock all the existing songs
        /// </summary>
        /// <returns>A list a the songs</returns>
        private List<Song> LoadSongs()
        {
            List<Song> songs = new List<Song>();
            string[] dirs = Directory.GetDirectories(this.SongsDirectory);
            int index = 0;
            foreach (string songDirectory in dirs)
            {
                string title = null, author = null, year = null;
                Uri file = null;
                string metadata = Path.Combine(songDirectory, "meta.data");
                string musicFile = Path.Combine(songDirectory, "music.mp3");

                // Check for metadata
                if (File.Exists(metadata)) {
                    string[] lines = File.ReadAllLines(metadata);
                    if (lines.Length == 3)
                    {
                        title = lines[0];
                        author = lines[1];
                        year = lines[2];
                    }
                }

                if (File.Exists(musicFile))
                {
                    file = new Uri(musicFile, UriKind.Relative);
                }

                // If metadata and music file were found, we can add sheets
                if (title != null && file != null)
                {
                    List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
                    string[] sheetsFiles = Directory.GetFiles(songDirectory, "*.sheet");
                    foreach (string sheet in sheetsFiles)
                    {
                        string[] split = Path.GetFileNameWithoutExtension(sheet).Split('-');
                        string instr = split[0];
                        string diff = split[1];
                        Instrument instrument = (Instrument)Enum.Parse(typeof(Instrument), instr, true);
                        Difficulty difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), diff, true);
                        sheets.Add(Tuple.Create(instrument, difficulty));
                    }

                    songs.Add(new Song(title, author, year, sheets, file, index));
                    index++;
                }
            }

            return songs;
        }

        /// <summary>
        /// Select a song
        /// Put the game up to date
        /// </summary>
        /// <param name="song">The selected song</param>
        public void SelectSong(Song song)
        {
            this.Game = new Game(song);
            this.SongSelected = true;

            foreach (Player player in this.Players)
            {
                player.InformNewGame();
            }
        }

        /// <summary>
        /// Add a player before or during the game
        /// </summary>
        /// <param name="player">The added player</param>
        public void NewPlayer(Player player)
        {
            this.Players.Add(player);
            if (this.SongSelected)
                player.InformNewGame();
        }

        /// <summary>
        /// Pauses the game
        /// </summary>
        public void Pause()
        {
            this.Game.Pause();
            foreach (Player player in this.Players)
            {
                player.Pause();
                foreach (Tuple<double,double,Note> noteInfo in player.SheetMusic.Notes)
                {
                    if (noteInfo.Item3.State == NoteState.Playing || noteInfo.Item3.State == NoteState.Resumed)
                    {
                        noteInfo.Item3.Pause();
                    }
                }
            }
        }

        /// <summary>
        /// Resumes the game
        /// Wait TIMEBEFORERESUME milliseconds before resuming
        /// </summary>
        public void Resume()
        {
            Timer timer = new Timer(GameMaster.TIMEBEFORERESUME);
            timer.Elapsed += delegate(object source, ElapsedEventArgs e)
            {
                this.Game.Resume();
                foreach (Player player in this.Players)
                {
                    player.Resume();
                    foreach (Tuple<double, double, Note> noteInfo in player.SheetMusic.Notes)
                    {
                        if (noteInfo.Item3.State == NoteState.Paused)
                        {
                            noteInfo.Item3.Resume();
                        }
                    }
                }
                timer.Close();
            };
            timer.Start();
        }
    }
}