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
        [TestMethod]
        public void ToHeight()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            List<Height> heights = new List<Height>();

            // Act
            foreach(string h in Enum.GetNames(typeof(Height)))
            {
                heights.Add(sb.ToHeight(h));
            }

            // Assert
            List<Height>.Enumerator e = heights.GetEnumerator();
            foreach(Height h in Enum.GetValues(typeof(Height)))
            {
                e.MoveNext();
                Assert.AreEqual(h, e.Current);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToHeightFail()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();

            // Act
            sb.ToHeight("adada");
        }

        [TestMethod]
        public void ToAccidental()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            List<Accidental> accidentals = new List<Accidental>();

            // Act
            foreach (string h in Enum.GetNames(typeof(Accidental)))
            {
                accidentals.Add(sb.ToAccidental(h));
            }

            // Assert
            List<Accidental>.Enumerator e = accidentals.GetEnumerator();
            foreach (Accidental h in Enum.GetValues(typeof(Accidental)))
            {
                e.MoveNext();
                Assert.AreEqual(h, e.Current);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToAccidentalFail()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();

            // Act
            sb.ToAccidental("adada");
        }

        [TestMethod]
        public void ToLength()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            List<Length> lengths = new List<Length>();

            // Act
            foreach (string h in Enum.GetNames(typeof(Length)))
            {
                lengths.Add(sb.ToLength(h));
            }

            // Assert
            List<Length>.Enumerator e = lengths.GetEnumerator();
            foreach (Length h in Enum.GetValues(typeof(Length)))
            {
                e.MoveNext();
                Assert.AreEqual(h, e.Current);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToLengthFail()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();

            // Act
            sb.ToLength("adada");
        }

        [TestMethod]
        public void FindFile()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();

            // Act
            string path = sb.FindFile("au clair de la lune", "voice", "beginner");

            // Assert
            Assert.AreNotEqual("", path);
            Assert.IsNotNull(path);
        }

        [TestMethod]
        public void FindFileFailed()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            int numberOfExceptionThrown = 0;

            // Act
            try { sb.FindFile("qqchose qui n'existe pas", "voice", "beginner"); }
            catch (DirectoryNotFoundException) { numberOfExceptionThrown++; }
            try { sb.FindFile("au clair de la lune", "voice", "incorrect"); }
            catch (FileNotFoundException) { numberOfExceptionThrown++; }
            try { sb.FindFile("au clair de la lune", "incorrect", "beginner"); }
            catch (FileNotFoundException) { numberOfExceptionThrown++; }
            try { sb.FindFile("au clair de la lune", "voice", "begin ner"); }
            catch (FileNotFoundException) { numberOfExceptionThrown++; }

            // Assert
            Assert.AreEqual(4, numberOfExceptionThrown);
        }

        [TestMethod]
        // Complete when bonuses ok
        public void BuildSheet()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            List<Instrument> instrs = new List<Instrument>();
            //instrs.Add(new Instrument("Voice"));
            Song s = new Song("au clair de la lune", null, null, Difficulty.Beginner, instrs);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Violin, Difficulty.Beginner);

            // Assert
            Assert.IsTrue(sm.Notes.Contains(sb.NoteFactory.GetNote(Length.Quarter, Accidental.None, Height.Do)));
        }

        [TestMethod]
        public void BuildSheetFailed()
        {
            // Arrange
            SheetBuilder sb = new SheetBuilder();
            List<Instrument> instrs = new List<Instrument>();
            //instrs.Add(new Instrument("Voice"));
            Song s = new Song("au clair de la lune", null, null, Difficulty.Beginner, instrs);

            // Act
            SheetMusic sm = sb.BuildSheet(s, Instrument.Guitar, Difficulty.Beginner);

            // Assert
            Assert.AreEqual(0, sm.Notes.Count);
            Assert.AreEqual(0, sm.Bonuses.Count);
        }
    }
}