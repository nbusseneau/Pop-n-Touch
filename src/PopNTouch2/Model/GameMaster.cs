using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    // Ajout d'un joueur à voir (le joueur s'ajoute, construit sa partition
    // mail il faut le rajouter en cours de route et éviter que le jeu ne se lance deux fois
    // comme c'est le cas actuellement
    public class GameMaster
    {
        private List<Player> players;
        private Game game;
        private Boolean upToDateGame;
        private SheetBuilder sheetBuilder;

        public List<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }

        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
            }
        }

        public Boolean UpToDateGame
        {
            get
            {
                return upToDateGame;
            }
            set
            {
                upToDateGame = value;
            }
        }

        public SheetBuilder SheetBuilder
        {
            get
            {
                return sheetBuilder;
            }
            set
            {
                sheetBuilder = value;
            }
        }

        public GameMaster()
        {
            players = new List<Player>();
            upToDateGame = false;
            sheetBuilder = new SheetBuilder();
        }

        public void selectSong(Song song)
        {
            Game = new Game(song);
            UpToDateGame = true;
            if (!Players.Any())
            {
                Players.Add(new Player(this));
            }
            foreach (Player player in Players)
            {
                player.CurrentGame = game;
                player.informNewGame();
            }
        }

        // Attribute ok ? See later
        public void newPlayer()
        {
            Player player = new Player(this);
            Players.Add(player);
            if (UpToDateGame)
                player.informNewGame();
        }

        public void ready()
        {
            Boolean everyoneReady = true;
            foreach (Player player in Players)
            {
                everyoneReady &= player.Ready;
            }
            if (everyoneReady)
                Game.launch();
        }

        public static void Main()
        {
            System.Console.WriteLine(0);
            GameMaster gameMaster = new GameMaster();
            List<Instrument> instrus = new List<Instrument>();
            instrus.Add(new Instrument("Saxo"));
            instrus.Add(new Instrument("Clarinette"));
            instrus.Add(new Instrument("Violon"));
            Song song = new Song("Chanson bidon", instrus);

            gameMaster.selectSong(song);
            System.Console.WriteLine(gameMaster.Game.Song.Name + ", " + gameMaster.Game.Song.Instruments);
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            gameMaster.newPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            foreach (Player player in gameMaster.Players)
            {
                player.iMReady();
            }

            gameMaster.newPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine("1 " + player.CurrentGame + ", " + player.SheetMusic);
                player.iMReady();
                System.Console.WriteLine("2 " + player.CurrentGame + ", " + player.SheetMusic);
            }

            while (true) { }
        }
    }
}