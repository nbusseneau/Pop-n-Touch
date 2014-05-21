using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public class Game
    {
        public Song Song { get; set; }
        public Boolean IsPlaying { get; set; }
        public int TimeElapsed { get; set; }
        public AudioController MusicPlayback { get; set; }

        public Game(Song s)
        {
            this.Song = s;
        }

        /// <summary>
        /// Launch the game 
        /// Launch the sheet reading by players
        /// Launch the time elapsed counter
        /// </summary>
        public void Launch()
        {
            this.IsPlaying = true;
            foreach (Player player in GameMaster.Instance.Players)
            {
                player.ReadSheet();
            }
            Timer timer = new Timer(1000);
            timer.Elapsed += delegate(object source, ElapsedEventArgs e)
            {
                this.TimeElapsed++;
            };
            timer.Start();
            this.MusicPlayback = new AudioController(Song.File, 3000);
        }

        /// <summary>
        /// Add a player in the middle of a game
        /// </summary>
        /// <param name="player">The player to add</param>
        // Maybe not very accurate
        public void AddPlayerInGame(Player player)
        {
            List<Tuple<double, double, Note>>.Enumerator enumerator = player.SheetMusic.Notes.GetEnumerator();
            double noteTime = 0;
            while(TimeElapsed > noteTime)
            {
                enumerator.MoveNext();
                noteTime = enumerator.Current.Item1;
            }
            enumerator.MoveNext();
            player.ReadSheet(true, enumerator);
        }
    }
}
