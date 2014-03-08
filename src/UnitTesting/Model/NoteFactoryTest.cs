using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PopNTouch2.Model;

namespace UnitTesting.Model
{
    [TestClass]
    public class NoteFactoryTest
    {
        [TestMethod]
        public void NoteFactoryLazyInstantiation()
        {
            // Arrange
            NoteFactory noteFactory = new NoteFactory();
            PrivateObject privateObject = new PrivateObject(noteFactory);
            Lazy<Note>[] notes = (Lazy<Note>[])privateObject.GetField("notes");
            Lazy<Note> lazyNote1 = notes[5]; // Length.whole = 0, Accidental.none = 0, Height.la = 5

            // Act
            bool isNoteInstantiated1 = lazyNote1.IsValueCreated;
            Note noteDirect = lazyNote1.Value;
            bool isNoteInstantiated2 = lazyNote1.IsValueCreated;
            Note noteGet = noteFactory.GetNote(Length.eighth, Accidental.sharp, Height.la);

            // Assert
            Assert.AreEqual(false, isNoteInstantiated1);
            Assert.AreEqual(true, isNoteInstantiated2);

            Assert.AreEqual(Length.whole, noteDirect.Length);
            Assert.AreEqual(Accidental.none, noteDirect.Accidental);
            Assert.AreEqual(Height.la, noteDirect.Height);

            Assert.AreEqual(Length.eighth, noteGet.Length);
            Assert.AreEqual(Accidental.sharp, noteGet.Accidental);
            Assert.AreEqual(Height.la, noteGet.Height);
        }
    }
}
