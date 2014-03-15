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
            Song s = new Song("song", null);
            Game g = new Game(s);

            // Act
            g.Launch();

            // Assert
            Assert.IsTrue(g.IsPlaying);
        }
    }
}