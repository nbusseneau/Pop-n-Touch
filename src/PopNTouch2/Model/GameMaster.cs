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

        public List<Player> Players {get; set;}
        public Game Game {get; set;}
        public Boolean UpToDateGame {get; set;}
        public SheetBuilder SheetBuilder {get; set;}

        public GameMaster()
        {
            players = new List<Player>();
            upToDateGame = false;
            sheetBuilder = new SheetBuilder();
        }

        public void SelectSong(Song song)
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
        public void NewPlayer()
        {
            Player player = new Player(this);
            Players.Add(player);
            if (UpToDateGame)
                player.informNewGame();
        }

        public void Ready()
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

            gameMaster.SelectSong(song);
            System.Console.WriteLine(gameMaster.Game.Song.Name + ", " + gameMaster.Game.Song.Instruments);
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            gameMaster.NewPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            foreach (Player player in gameMaster.Players)
            {
                player.iMReady();
            }

            gameMaster.NewPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine("1 " + player.CurrentGame + ", " + player.SheetMusic);
                player.iMReady();
                System.Console.WriteLine("2 " + player.CurrentGame + ", " + player.SheetMusic);
            }

            System.Console.ReadLine();
        }
    }
}