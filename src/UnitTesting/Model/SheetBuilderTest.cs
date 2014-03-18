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
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(Instrument.Guitar);
            Song s = new Song("au clair de la lune", null, null, Difficulty.Beginner, instrs);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Guitar, Difficulty.Beginner);

            // Assert
            Assert.IsTrue(sm.Notes.Contains(sb.NoteFactory.GetNote(Length.Quarter, Accidental.None, Height.Do)));
        }

        [TestMethod]
        public void BuildSheetFailed()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder(path);
            List<Instrument> instrs = new List<Instrument>();
            instrs.Add(Instrument.Violin);
            Song s = new Song("au clair de la lune", null, null, Difficulty.Beginner, instrs);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Violin, Difficulty.Expert);

            // Assert
            Assert.AreEqual(0, sm.Notes.Count);
            Assert.AreEqual(0, sm.Bonuses.Count);
        }
    }
}