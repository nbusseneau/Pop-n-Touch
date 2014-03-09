using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;

namespace UnitTesting.Model
{
    [TestClass]
    public class GameMasterTest
    {
        public GameMaster gameMaster = new GameMaster();
        public Song song = new Song("Blabla", new System.Collections.Generic.List<Instrument>());

        [TestMethod]
        public void SelectSongNormal()
        {
            gameMaster.SelectSong(song);
            Assert.AreEqual(song, gameMaster.Game.Song);
        }

        [TestMethod]
        public void SelectSongTwice()
        {
            gameMaster.SelectSong(song);
            Song song2 = new Song("2", new System.Collections.Generic.List<Instrument>());
            gameMaster.SelectSong(song2);
            foreach (Player player in gameMaster.Players)
            {
                Assert.AreEqual(song2.Instruments, player.AvailableInstruments);
            }
        }

        [TestMethod]
        public void NewPlayer()
        {
            // Arrange
            int numberOfPlayers = 0;
            gameMaster.UpToDateGame = true;
            List<Instrument> instrus = new List<Instrument>();
            instrus.Add(new Instrument("Saxo"));
            instrus.Add(new Instrument("Clarinette"));
            Song song = new Song("Chanson bidon", instrus);
            gameMaster.Game = new Game(song);

            // Act
            gameMaster.NewPlayer();
            foreach (Player p in gameMaster.Players)
            {
                numberOfPlayers++;
            }

            // Assert
            Assert.AreEqual(1, numberOfPlayers);
            foreach (Player p in gameMaster.Players)
            {
                Assert.AreEqual(p.GameMaster, gameMaster);
                Assert.IsNull(p.SheetMusic);
                Assert.AreEqual(p.CurrentGame, gameMaster.Game);
                Assert.IsFalse(p.Ready);
                Assert.AreEqual(p.AvailableInstruments, instrus);
                Assert.IsNull(p.Instrument);
                Assert.IsInstanceOfType(p.Difficulty, typeof(Difficulty));
            }
        }

        [TestMethod]
        public void Ready()
        {
            gameMaster.SelectSong(song);
            gameMaster.NewPlayer();
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