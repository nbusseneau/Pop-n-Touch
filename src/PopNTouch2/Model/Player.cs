using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class Player
    {
        private GameMaster gameMaster;
        private SheetMusic sheetmusic;
        private Game currentGame;
        private Boolean ready;
        private List<Instrument> availableInstruments;
        private Instrument instrument;
        private Difficulty difficulty;

        public GameMaster GameMaster
        {
            get
            {
                return gameMaster;
            }
            set
            {
                gameMaster = value;
            }
        }

        public SheetMusic SheetMusic
        {
            get
            {
                return sheetmusic;
            }
            set
            {
                sheetmusic = value;
            }
        }

        public Game CurrentGame
        {
            get
            {
                return currentGame;
            }
            set
            {
                currentGame = value;
            }
        }

        public Boolean Ready
        {
            get
            {
                return ready;
            }
            set
            {
                ready = value;
            }
        }

        public List<Instrument> AvailableInstruments
        {
            get
            {
                return availableInstruments;
            }
            set
            {
                availableInstruments = value;
            }
        }

        public Instrument Instrument
        {
            get
            {
                return instrument;
            }
            set
            {
                instrument = value;
            }
        }

        public Difficulty Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                difficulty = value;
            }
        }

        public Player(GameMaster gameMaster)
        {
            GameMaster = gameMaster;
        }

        public void informNewGame()
        {
            CurrentGame = GameMaster.Game;
            AvailableInstruments = CurrentGame.Song.Instruments;
        }

        public void iMReady()
        {
            SheetMusic = GameMaster.SheetBuilder.buildSheet(GameMaster.Game.Song, Instrument, Difficulty);
            ready = true;
            GameMaster.ready();
        }

        public void notReadyAnymore()
        {
            ready = false;
        }
    }
}
