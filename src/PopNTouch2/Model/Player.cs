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
            GameMaster = gameMaster;
        }

        public void InformNewGame()
        {
            CurrentGame = GameMaster.Game;
            AvailableInstruments = CurrentGame.Song.Instruments;
        }

        public void IMReady()
        {
            SheetMusic = GameMaster.SheetBuilder.buildSheet(GameMaster.Game.Song, Instrument, Difficulty);
            Ready = true;
            GameMaster.Ready();
        }

        public void NotReadyAnymore()
        {
            Ready = false;
        }
    }
}
