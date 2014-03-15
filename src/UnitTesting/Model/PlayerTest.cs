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
            GameMaster gm = new GameMaster();
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(new Instrument("Clar"));
            instrs.Add(new Instrument("Voix"));
            Song s = new Song("song", instrs);
            Game g = new Game(s);
            Player p = new Player(gm);
            gm.Game = g;

            // Act
            p.InformNewGame();

            // Assert
            Assert.AreEqual(p.GameMaster, gm);
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsFalse(p.Ready);
            Assert.AreEqual(s.Instruments, p.AvailableInstruments);
        }

        [TestMethod]
        public void IMReady()
        {
            // Arrange
            GameMaster gm = new GameMaster();
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(new Instrument("Clar"));
            Instrument voix = new Instrument("Voice");
            //instrs.Add(new Instrument("Voix"));
            instrs.Add(voix);
            Song s = new Song("au clair de la lune", instrs);
            Player p = new Player(gm);

            gm.SelectSong(s);
            p.InformNewGame();

            p.Instrument = voix;
            p.Difficulty = Difficulty.beginner;

            // Act
            p.IMReady();

            // Assert
            Assert.AreEqual(p.GameMaster, gm);
            Assert.IsNotNull(p.SheetMusic);
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsTrue(p.Ready);
            Assert.AreEqual(s.Instruments, p.AvailableInstruments);
            Assert.IsNotNull(p.Instrument);
            Assert.IsNotNull(p.Difficulty);
        }

        [TestMethod]
        public void NotReallyReady()
        {
            // Arrange
            GameMaster gm = new GameMaster();
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(new Instrument("Clar"));
            instrs.Add(new Instrument("Voix"));
            Song s = new Song("song", instrs);
            Player p = new Player(gm);

            gm.SelectSong(s);
            p.InformNewGame();

            int numberOfExceptionsThrown = 0;

            // Act
            try { p.IMReady(); }
            catch (ArgumentException) { numberOfExceptionsThrown++; }
            p.Instrument = new Instrument("Voix");
            try { p.IMReady(); }
            catch (ArgumentException) { numberOfExceptionsThrown++; }
            p.Instrument = new Instrument("Saxo");
            p.Difficulty = Difficulty.expert;
            try { p.IMReady(); }
            catch (ArgumentException) { numberOfExceptionsThrown++; }

            // Assert
            Assert.AreEqual(3, numberOfExceptionsThrown);
        }

        public void NotReadyAnymore()
        {
            // Arrange
            GameMaster gm = new GameMaster();
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(new Instrument("Clar"));
            instrs.Add(new Instrument("Voix"));
            Song s = new Song("song", instrs);
            Player p = new Player(gm);

            gm.SelectSong(s);
            p.InformNewGame();

            // Act
            //p.IMReady();
            p.NotReadyAnymore();

            // Assert
            Assert.AreEqual(p.GameMaster, gm);
            Assert.IsNotNull(p.SheetMusic);
            Assert.AreEqual(p.CurrentGame, gm.Game);
            Assert.IsFalse(p.Ready);
            Assert.AreEqual(s.Instruments, p.AvailableInstruments);
        }
    }
}
