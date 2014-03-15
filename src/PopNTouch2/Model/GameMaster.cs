using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    public class GameMaster
    {
        /*private List<Player> players;
        private Game game;
        private Boolean upToDateGame;
        private SheetBuilder sheetBuilder;*/

        public List<Player> Players { get; set; }
        public Game Game { get; set; }
        public Boolean UpToDateGame { get; set; }
        public SheetBuilder SheetBuilder { get; set; }

        public GameMaster()
        {
            this.Players = new List<Player>();
            this.UpToDateGame = false;
            this.SheetBuilder = new SheetBuilder();
        }

        public void SelectSong(Song song)
        {
            this.Game = new Game(song);
            this.UpToDateGame = true;
            if (!this.Players.Any())
            {
                this.Players.Add(new Player(this));
            }
            foreach (Player player in this.Players)
            {
                player.InformNewGame();
            }
        }

        // Attribute ok ? See later
        public void NewPlayer()
        {
            Player player = new Player(this);
            this.Players.Add(player);
            if (this.UpToDateGame)
                player.InformNewGame();
        }

        // Called by a player > at least one player existing and ready
        public void Ready()
        {
            Boolean everyoneReady = true;
            foreach (Player player in this.Players)
            {
                everyoneReady &= player.Ready;
            }
            if (everyoneReady && !this.Game.IsPlaying)
                this.Game.Launch();
            else if (everyoneReady && this.Game.IsPlaying)
                ; // Place the sheet reader to the right part of the sheet then play
        }
    }
}