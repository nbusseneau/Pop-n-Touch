using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;

namespace UnitTesting.Model
{
    [TestClass]
    public class GameMasterTest
    {
        public Song song = new Song("Blabla", null, null, new List<Tuple<Instrument, Difficulty>>(), null);

        [TestMethod]
        public void LoadSongs()
        {
            // Arrange
            GameMaster gameMaster = GameMaster.Instance;
            PrivateObject privateObject = new PrivateObject(gameMaster);

            // Act
            List<Song> songs = (List<Song>)privateObject.Invoke("LoadSongs");
            Song AuClair = songs.Find((song) => (song.Title == "Au Clair de la Lune"));

            // Assert
            Assert.AreEqual("Au Clair de la Lune", AuClair.Title);
            Assert.AreEqual("Anonyme", AuClair.Author);
            Assert.AreEqual("17XX", AuClair.Year);
        }

        [TestMethod]
        public void SelectSongNormal()
        {
            GameMaster gameMaster = GameMaster.Instance;
            gameMaster.SelectSong(song);
            Assert.AreEqual(song, gameMaster.Game.Song);
        }

        [TestMethod]
        public void SelectSongTwice()
        {
            GameMaster gameMaster = GameMaster.Instance;
            gameMaster.SelectSong(song);
            foreach (Player p in gameMaster.Players)
            {
                p.Ready = true;
            }
            Song song2 = new Song("2", null, null, new List<Tuple<Instrument, Difficulty>>(), null);
            gameMaster.SelectSong(song2);
            foreach (Player player in gameMaster.Players)
            {
                Assert.IsFalse(player.Ready);
            }
        }

        [TestMethod]
        public void NewPlayer()
        {
            // Arrange
            GameMaster gameMaster = GameMaster.Instance;
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song song = new Song("Chanson bidon", null, null, sheets, null);
            gameMaster.SelectSong(song);
            int numberOfPlayers = 0;
            foreach (Player p in gameMaster.Players)
            {
                numberOfPlayers++;
            }

            // Act
            gameMaster.NewPlayer(new Player());
            int numberOne = 0;
            foreach (Player p in gameMaster.Players)
            {
                numberOne++;
            }

            // Assert
            Assert.AreEqual(numberOfPlayers+1, numberOne);
            foreach (Player p in gameMaster.Players)
            {
                Assert.IsNull(p.SheetMusic);
                Assert.AreEqual(p.CurrentGame, gameMaster.Game);
                Assert.IsFalse(p.Ready);
                Assert.IsNotNull(p.Instrument);
                Assert.IsInstanceOfType(p.Difficulty, typeof(Difficulty));
            }
        }

        [TestMethod]
        public void Ready()
        {
            GameMaster gameMaster = GameMaster.Instance;
            gameMaster.SelectSong(song);
            gameMaster.NewPlayer(new Player());
            gameMaster.Ready();
            Assert.IsFalse(gameMaster.Game.IsPlaying);

            foreach (Player p in gameMaster.Players)
            {
                p.Ready = true;
            }
            gameMaster.Ready();
            Assert.IsTrue(gameMaster.Game.IsPlaying);

            /*gameMaster.NewPlayer();
            foreach (Player p in gameMaster.Players)
            {
                p.Ready = true;
            }
            gameMaster.Ready();
            Assert.Inconclusive("Adding a player in a playing game not yet implemented.");
            */
        }
    }
}