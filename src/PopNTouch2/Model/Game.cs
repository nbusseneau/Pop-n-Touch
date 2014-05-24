using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Game
    {
        public Song Song { get; set; }
        public Boolean IsPlaying { get; set; }
        //public int TimeElapsed { get; set; }
        public Stopwatch TimeElapsed { get; set; }
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
                player.ResetScores();
                player.ReadSheet();
            }
            /*Timer timer = new Timer(1000);
            timer.Elapsed += delegate(object source, ElapsedEventArgs e)
            {
                this.TimeElapsed++;
            };
            timer.Start();*/
            this.TimeElapsed = new Stopwatch();
            this.TimeElapsed.Start();
            // Delay of playback ?
            this.MusicPlayback = new AudioController(Song.File, GameMaster.TIMETOPLAY+500);
            this.MusicPlayback.MediaEnded += new EventHandler(AudioFinished);
        }

        /// <summary>
        /// Pause the sound and the timers associated
        /// </summary>
        public void Pause()
        {
            this.MusicPlayback.Pause();
            this.TimeElapsed.Stop();
        }

        /// <summary>
        /// Resume the sound and the timers associated after a certain period of time
        /// </summary>
        public void Resume()
        {
            this.MusicPlayback.Dispatcher.Invoke((Action)(() => { this.MusicPlayback.Play(); }));
            this.TimeElapsed.Start();
        }

        /// <summary>
        /// Add a player in the middle of a game
        /// Can only be done when the game is paused
        /// </summary>
        /// <param name="player">The player to add</param>
        // Maybe not very accurate
        public void AddPlayerInGame(Player player)
        {
            this.TimeElapsed.Stop();
            long time = this.TimeElapsed.ElapsedMilliseconds;

            List<Tuple<double, double, Note>>.Enumerator enumerator = player.SheetMusic.Notes.GetEnumerator();
            double noteTime = 0;
            while(time > noteTime + GameMaster.TIMETOPLAY)
            {
                enumerator.MoveNext();
                noteTime = enumerator.Current.Item1;
            }
            enumerator.MoveNext();
            player.ReadSheet(true, enumerator);
        }

        /// <summary>
        /// Do what to do when the song is finished
        /// </summary>
        public void AudioFinished(object sender, EventArgs e)
        {
            this.IsPlaying = false;
            this.MusicPlayback.Close();
            this.TimeElapsed.Reset();
        }
    }
}
