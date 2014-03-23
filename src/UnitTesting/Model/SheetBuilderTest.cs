using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;
using System.Collections.Generic;
using System.IO;

namespace UnitTesting.Model
{
    [TestClass]
    public class SheetBuilderTest
    {
        public string path = @"Resources\Songs\";
        [TestMethod]
        // Complete when bonuses ok
        public void BuildSheet()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder(path);
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Guitar, Difficulty.Beginner));
            Song s = new Song("au clair de la lune", null, null, sheets, 90);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Guitar, Difficulty.Beginner);

            // Assert
            Assert.AreNotEqual(0, sm.Notes.Count);
        }

        [TestMethod]
        public void BuildSheetFailed()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder(path);
            List<Tuple<Instrument, Difficulty>> sheets = new List<Tuple<Instrument, Difficulty>>();
            sheets.Add(Tuple.Create(Instrument.Violin, Difficulty.Beginner));
            Song s = new Song("au clair de la lune", null, null, sheets, 90);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Violin, Difficulty.Expert);

            // Assert
            Assert.AreEqual(0, sm.Notes.Count);
            Assert.AreEqual(0, sm.Bonuses.Count);
        }
    }
}