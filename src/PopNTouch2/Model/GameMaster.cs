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
            Players = new List<Player>();
            UpToDateGame = false;
            SheetBuilder = new SheetBuilder();
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
                player.InformNewGame();
            }
        }

        // Attribute ok ? See later
        public void NewPlayer()
        {
            Player player = new Player(this);
            Players.Add(player);
            if (UpToDateGame)
                player.InformNewGame();
        }

        // Called by a player > at least one player existing and ready
        public void Ready()
        {
            Boolean everyoneReady = true;
            foreach (Player player in Players)
            {
                everyoneReady &= player.Ready;
            }
            if (everyoneReady && !Game.IsPlaying)
                Game.Launch();
            else if (everyoneReady && Game.IsPlaying)
                ; // Rajouter le joueur dans la partie (construction différente de la partoche ?)
        }

        public static void Main()
        {
            GameMaster gameMaster = new GameMaster();
            List<Instrument> instrus = new List<Instrument>();
            instrus.Add(new Instrument("Saxo"));
            instrus.Add(new Instrument("Clarinette"));
            instrus.Add(new Instrument("Violon"));
            Song song = new Song("Chanson bidon", instrus);

            gameMaster.SelectSong(song);
            System.Console.WriteLine("La chanson est " + gameMaster.Game.Song.Name + " avec les instruments " + gameMaster.Game.Song.Instruments);
            System.Console.WriteLine("Les joueurs sont : ");
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            System.Console.WriteLine("\nOn rajoute un nouveau joueur. Les joueurs sont : ");
            gameMaster.NewPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine(player);
            }

            System.Console.WriteLine("\nTout le monde est prêt.");
            foreach (Player player in gameMaster.Players)
            {
                player.IMReady();
            }

            System.Console.WriteLine("\nOn rajoute un nouveau joueur. Les joueurs sont " + 
            "(1 : joueur, partition), prêt, (2 : joueur, partition) :");
            gameMaster.NewPlayer();
            foreach (Player player in gameMaster.Players)
            {
                System.Console.WriteLine("1 " + player.GameMaster.Game + ", " + player.SheetMusic);
                player.IMReady();
                System.Console.WriteLine("2 " + player.GameMaster.Game + ", " + player.SheetMusic);
            }

            System.Console.WriteLine("La partition n'est créée que quand le joueur est prêt : pas d'affichage de partition existante avant.");
            
            while (true) { }
        }
    }
}