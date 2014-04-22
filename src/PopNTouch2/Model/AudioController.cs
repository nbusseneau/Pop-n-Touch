using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="playImmediately">If true, the file should be played as soon as loaded.</param>
        /// </summary> 
        public AudioController(Uri filepath, bool playImmediately=false) : base()
        {
            this.Open(filepath);
            if (playImmediately)
            {
                this.Play();
            }
        }

        /// <summary>
        /// Instantiate an AudioController.
        /// <param name="song">Song to be opened.</param>
        /// <param name="playImmediately">If true, the file should be played as soon as loaded. Defaults to false.</param>
        /// </summary> 
        public AudioController(Song song, bool playImmediately=false) : this(song.File, playImmediately)
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
