using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Game
    {
        /*private Song song;
        private Boolean isPlaying;*/

        public Song Song { get; set; }
        public Boolean IsPlaying { get; set; }

        public Game(Song s)
        {
            this.Song = s;
        }

        public void Launch()
        {
            this.IsPlaying = true;
            System.Console.WriteLine("Le jeu se lance !");
        }
    }
}
