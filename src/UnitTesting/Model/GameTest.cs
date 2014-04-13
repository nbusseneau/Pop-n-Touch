using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;

namespace UnitTesting.Model
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Launch()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            Player player = new Player();
            gm.NewPlayer(player);
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Piano, Difficulty.Classic));
            Song newBorn = new Song("New Born", "Muse", "2001", sheets);
            gm.SelectSong(newBorn);
            player.Instrument = Instrument.Piano;
            player.Difficulty = Difficulty.Classic;
            player.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, player.Instrument, player.Difficulty);
            player.IMReady();

            // Act
            //System.Threading.Thread.Sleep(1);
            gm.Game.Launch();

            // Assert
            Assert.IsTrue(gm.Game.IsPlaying);
        }

        [TestMethod]
        public void AddPlayerInGame()
        {
            // Arrange
            GameMaster gm = GameMaster.Instance;
            Player player = new Player();
            gm.NewPlayer(player);
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument,Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Piano, Difficulty.Classic));
            Song newBorn = new Song("New Born", "Muse", "2001", sheets);
            gm.SelectSong(newBorn);
            player.Instrument = Instrument.Piano;
            player.Difficulty = Difficulty.Classic;
            player.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, player.Instrument, player.Difficulty);
            player.IMReady();
            gm.Ready();
            Player p2 = new Player();
            p2.Instrument = Instrument.Piano;
            p2.Difficulty = Difficulty.Classic;
            p2.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, player.Instrument, player.Difficulty);
            System.Threading.Thread.Sleep(2000);

            // Act
            gm.Game.AddPlayerInGame(p2);

            // Assert
            Assert.IsTrue(gm.Game.IsPlaying);
            Assert.IsTrue(gm.Game.TimeElapsed >= 1);
        }
    }
}