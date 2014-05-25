using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class SheetMusic
    {
        // Represents a triplet (time when the note should appear, time it should be played, note)
        public List<Tuple<double, double, Note>> Notes { get; set; }
        public List<IBonus> Bonuses { get; set; }

        /// <summary>
        /// Creates a new SheetMusic with empty Notes and Bonuses
        /// </summary>
        public SheetMusic()
        {
            this.Notes = new List<Tuple<double, double, Note>>();
            this.Bonuses = new List<IBonus>();
        }

        /// <summary>
        /// Returns maximum score one can get with this Sheet
        /// </summary>
        /// <returns></returns>
        public int GetMaxScore()
        {
            return this.Notes.Count * Player.PERFECT_SCORE;
        }
    }
}

