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
            Lazy<Note> lazyNote = notes[5]; // Length.Whole = 0, Accidental.None = 0, Height.La = 5

            // Act
            bool isNoteInstantiatedBefore = lazyNote.IsValueCreated;
            Note noteDirect = lazyNote.Value;
            bool isNoteInstantiatedAfter = lazyNote.IsValueCreated;
            Note noteGet = noteFactory.GetNote(Length.Eighth, Accidental.Sharp, Height.La);

            // Assert
            Assert.AreEqual(false, isNoteInstantiatedBefore);
            Assert.AreEqual(true, isNoteInstantiatedAfter);

            Assert.AreEqual(Length.Whole, noteDirect.Length);
            Assert.AreEqual(Accidental.None, noteDirect.Accidental);
            Assert.AreEqual(Height.La, noteDirect.Height);

            Assert.AreEqual(Length.Eighth, noteGet.Length);
            Assert.AreEqual(Accidental.Sharp, noteGet.Accidental);
            Assert.AreEqual(Height.La, noteGet.Height);
        }
    }
}
