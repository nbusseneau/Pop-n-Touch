using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Player
    {
        /*private GameMaster gameMaster;
        private SheetMusic sheetmusic;
        private Game currentGame;
        private Boolean ready;
        private List<Instrument> availableInstruments;
        private Instrument instrument;
        private Difficulty difficulty; */

        public GameMaster GameMaster { get; set; }
        public SheetMusic SheetMusic { get; set; }
        public Game CurrentGame { get; set; }
        public Boolean Ready { get; set; }
        public List<Instrument> AvailableInstruments { get; set; }
        public Instrument Instrument { get; set; }
        public Difficulty Difficulty { get; set; }

        public Player(GameMaster gameMaster)
        {
            this.GameMaster = gameMaster;
        }

        public void InformNewGame()
        {
            this.CurrentGame = GameMaster.Game;
            this.AvailableInstruments = CurrentGame.Song.Instruments;
            this.Ready = false;
        }

        public void IMReady()
        {
            if (this.CurrentGame.Song.Instruments.Contains(this.Instrument) && this.Difficulty != null)
            {
                this.SheetMusic = GameMaster.SheetBuilder.BuildSheet(GameMaster.Game.Song, Instrument, Difficulty);
                this.Ready = true;
                GameMaster.Ready();
            }
            else throw new System.ArgumentException("No difficulty and/or wrong instrument.");
        }

        public void NotReadyAnymore()
        {
            this.Ready = false;
        }
    }
}
