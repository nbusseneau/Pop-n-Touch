using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;
using System.Timers;
using System.Threading;

namespace UnitTesting.Model
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void InformNewGame()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song s = new Song("song", null, null, sheets);
            Player p = new Player();
            gm.SelectSong(s);

            // Act
            p.InformNewGame();

            // Assert
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsFalse(p.Ready);
        }

        [TestMethod]
        public void IMReady()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song s = new Song("au clair de la lune", null, null, sheets);
            Player p = new Player();

            gm.SelectSong(s);
            p.InformNewGame();

            p.Instrument = Instrument.Violin;
            p.Difficulty = Difficulty.Beginner;

            // Act
            p.IMReady();

            // Assert
            Assert.IsNotNull(p.SheetMusic);
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsTrue(p.Ready);
            Assert.IsNotNull(p.Instrument);
            Assert.IsNotNull(p.Difficulty);
        }

        [TestMethod]
        public void NotReadyAnymore()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song s = new Song("song", null, null, sheets);
            Player p = new Player();

            gm.SelectSong(s);
            p.InformNewGame();

            // Act
            //p.IMReady();
            p.NotReadyAnymore();

            // Assert
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsFalse(p.Ready);
        }

        [TestMethod]
        public void ReadSheet()
        {
            // Arrange
            int eventFired = 0;
            GameMaster gm = GameMaster.Instance;
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song s = new Song("Au clair de la lune", null, null, sheets);
            Player p = new Player();
            gm.SelectSong(s);
            p.InformNewGame();
            p.Instrument = Instrument.Guitar;
            p.Difficulty = Difficulty.Beginner;
            p.IMReady();
            p.Tick += delegate(Player sender, Player.NoteTicked nt)
            {
                eventFired++;
            };

            // Act
            p.ReadSheet();
            Thread.Sleep(3000);

            // Assert
            Assert.AreNotEqual(0, eventFired);
        }

        [TestMethod]
        public void NoteScored()
        {
            // Arrange
            Player p = new Player();

            // Act
            for (double d = 0; d < 1.5; d += 0.1)
            {
                p.NoteScored(d);
            }

            // Assert
            Assert.AreEqual(50, p.Score);
        }
    }
}
