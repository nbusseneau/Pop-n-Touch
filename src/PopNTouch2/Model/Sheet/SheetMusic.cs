using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class SheetMusic
    {
        public double FirstRest { get; set; }
        // Represents a couple (time when the next note should appear relatively to the current note ~ length of the current note, note)
        public List<Tuple<double, Note>> Notes { get; set; }
        public List<IBonus> Bonuses { get; set; }
        public int MaxScore { get; set; }

        public SheetMusic()
        {
            this.FirstRest = 0;
            this.Notes = new List<Tuple<double, Note>>();
            this.Bonuses = new List<IBonus>();
            this.MaxScore = 0;
        }
    }
}

