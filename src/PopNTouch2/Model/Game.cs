using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Game
    {
        private Song song;

        public Song Song
        {
            get
            {
                return song;
            }
            set
            {
                song = value;
            }
        }

        public Game(Song song)
        {
            Song = song;
        }

        public void launch()
        {
            System.Console.WriteLine("Le jeu se lance !");
        }
    }
}
