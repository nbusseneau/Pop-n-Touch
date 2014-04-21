using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PopNTouch2.Model
{
    /// <summary>
    /// Class responsible for playing sounds
    /// Inherits from MediaPlayer
    /// </summary>
    public class AudioController : MediaPlayer
    {
        public AudioController() : base()
        {
        }

        public AudioController(Song song) : base()
        {
            this.Open(song.File);
        }

        public void Restart()
        {
            this.Stop();
            this.Play();
        }
    }
}
