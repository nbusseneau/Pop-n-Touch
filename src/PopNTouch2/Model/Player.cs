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

        public SheetMusic SheetMusic { get; set; }
        public Game CurrentGame { get; set; }
        public Boolean Ready { get; set; }
        public Instrument Instrument { get; set; }
        public Difficulty Difficulty { get; set; }

        public Player() { }

        public void InformNewGame()
        {
            this.CurrentGame = GameMaster.Instance.Game;
            this.Ready = false;
        }

        public void IMReady()
        {
            this.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, Instrument, Difficulty);
            this.Ready = true;
            GameMaster.Instance.Ready();
        }

        public void NotReadyAnymore()
        {
            this.Ready = false;
        }
    }
}
