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
        int playersFinished { get; set; }

        public Game(Song s)
        {
            this.Song = s;
            playersFinished = 0;
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

        /// <summary>
        /// Launch an event when the game is finished (song finished & sheets read)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GameFinishedHandler(object sender, EventArgs e);
        public event GameFinishedHandler GameFinishedEvent;

        /// <summary>
        /// Count number of player who have finished and launch the GameFinishedEvent
        /// </summary>
        public void PlayerFinished()
        {
            playersFinished++;
            if (playersFinished == GameMaster.Instance.Players.Count)
            {
                this.IsPlaying = false;
                if (GameFinishedEvent != null)
                {
                    GameFinishedEvent(this, null);
                }
            }
        }
    }
}
