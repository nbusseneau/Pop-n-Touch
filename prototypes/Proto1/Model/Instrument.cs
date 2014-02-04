using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Microsoft.Xna.Framework.Audio;
using System.Windows;

namespace Proto1 {

	public enum InstrumentType {
		bass = 1,
		clarinette = 2,
		contrebass = 3,
		flute = 4,
		piano = 5,
		saxo = 6,
		vibraphone = 7,
		violon = 8,

	}

	[Serializable]
	/// <summary>
	/// Defines an instrument and methods to play sounds with it.
	/// </summary>
	public class Instrument {
		/// <summary>
		/// Property.
		/// Type of an instrument.
		/// </summary>
		public InstrumentType Name { get; set; }

		/// <summary>
		/// Property.
		/// Session's Tempo
		/// </summary>
		public int Bpm { get; set; }

		#region constructors
		/// <summary>
		/// Instrument Constructor.
		/// Generates an object of class Instrument with a given type.
		/// </summary>
		/// <param name="instru">The type for the new instrument</param>
		public Instrument(InstrumentType instru) {
			Name = instru;
			Bpm = 90;
		}
		#endregion

		#region methods

		/// <summary>
		/// Set the tempo of an instrument
		/// </summary>
		/// <param name="newBpm"></param>
		public void SetBpm(int newBpm) {
			Bpm = newBpm;
		}

		/// <summary>
		/// Plays the given note with the instrument.
		/// </summary>
		/// <param name="n">The note to play</param>
		public void PlayNote(Note n) {
			Instrument tmp = new Instrument(Name);
			Thread t = new Thread(tmp.ActionPlay);
			t.Start(n);
		}

		/// <summary>
		/// Called by PlayNote(Note n)
		/// The action of playing a sound during the TimeSpan t
		/// </summary>
		/// <param name="n">The Note to play</param>
		public void ActionPlay(object n) {
			Note note = n as Note;
			TimeSpan t = new TimeSpan(0, 0, 0, 0, (note.Duration.GetHashCode() * 30000) / Bpm);
			
			Cue cue = AudioController.INSTANCE.SoundBank.GetCue("silence");
			try {
				cue = AudioController.INSTANCE.SoundBank.GetCue(Name.ToString() + "_" + note.GetCue());
			} catch (Exception e) {
				Console.WriteLine("Exception with the note placed on the stave");
			}
			AudioController.PlaySound(cue);
			Thread.Sleep(t);
			AudioController.StopSound(cue);
		}
		#endregion
	}
}
