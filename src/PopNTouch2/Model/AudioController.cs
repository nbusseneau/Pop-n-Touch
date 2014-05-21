using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Media;

namespace PopNTouch2.Model
{
    /// <summary>
    /// Class responsible for playing sounds. Inherits from MediaPlayer.
    /// </summary>
    public class AudioController : MediaPlayer
    {
        /// <summary>
        /// Instantiate an AudioController.
        /// <param name="filepath">Path of the file to be opened.</param>
        /// <param name="delayStart">If set, wait for delayStart milliseconds before starting.</param>
        /// </summary> 
        public AudioController(Uri filepath, double delayStart = 0d) : base()
        {
            this.Open(filepath);
            if (delayStart == 0d)
            {
                this.Play();
            }
            else
            {
                Timer timer = new Timer(delayStart);
                timer.Elapsed += delegate(object source, ElapsedEventArgs e)
                {
                    this.Dispatcher.Invoke((Action)(() => { this.Play(); })); ;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        /// <summary>
        /// Instantiate an AudioController.
        /// <param name="song">Song to be opened.</param>
        /// <param name="delayStart">If set, wait for delayStart milliseconds before starting.</param>
        /// </summary> 
        public AudioController(Song song, double delayStart = 0d) : this(song.File, delayStart)
        {
        }

        /// <summary>
        /// Restart playback of opened sound.
        /// </summary>
        public void Restart()
        {
            this.Stop();
            this.Play();
        }
    }
}
