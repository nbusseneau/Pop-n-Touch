using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;

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
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(Instrument.Guitar);
            instrs.Add(Instrument.Violin);
            Song s = new Song("song", null, null, Difficulty.Beginner, instrs);
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
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(Instrument.Guitar);
            Instrument violin = Instrument.Violin;
            instrs.Add(violin);
            Song s = new Song("au clair de la lune", null, null, Difficulty.Beginner, instrs);
            Player p = new Player();

            gm.SelectSong(s);
            p.InformNewGame();

            p.Instrument = violin;
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

        public void NotReadyAnymore()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(Instrument.Guitar); //instrs.Add(new Instrument("Clar"));
            instrs.Add(Instrument.Violin); //instrs.Add(new Instrument("Voix"));
            Song s = new Song("song", null, null, Difficulty.Beginner, instrs);
            Player p = new Player();

            gm.SelectSong(s);
            p.InformNewGame();

            // Act
            //p.IMReady();
            p.NotReadyAnymore();

            // Assert
            Assert.IsNotNull(p.SheetMusic);
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsFalse(p.Ready);
        }
    }
}
