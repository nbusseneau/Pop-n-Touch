using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Microsoft.Xna.Framework.Media;

namespace Proto1 {
	public class Melody {
		public ObservableCollection<Note> Notes { get; set; }

		public List<NoteView> NotesView { get; set; }

		public Instrument CurrentInstrument { get; set; }

		public int MaxPosition { get; set; }

		public int IteratorNotes { get; set; }

		private DispatcherTimer Timer;

		public int PositionNote { get; set; }

		private bool isPlaying;


		public Melody(Instrument instru)
        {
            MaxPosition = 0;
            Notes = new ObservableCollection<Note>();
			NotesView = new List<NoteView>();
            CurrentInstrument = instru;
            Timer = new DispatcherTimer();
            IteratorNotes = 0;
            PositionNote = 0;
            Microsoft.Xna.Framework.FrameworkDispatcher.Update();
        }

		/// <summary>
		/// Adds a Note on the stave at a defined position.
		/// </summary>
		/// <param name="note">The note to be added</param>
		/// <param name="position">The Note position</param>
		public void AddNote(Note note, int position) {
			note.Position = position;
			if (!Notes.Contains(note)) {
				int i = 0;
				for (i = 0; (i < Notes.Count && Notes[i].Position < position); i++) ;

				Notes.Insert(i, note);
				MaxPosition = Math.Max(MaxPosition, note.Position);
			}
		}

		/// <summary>
		/// Remove the note from the list Notes
		/// </summary>
		/// <param name="note">The note to remove</param>
		public void RemoveNote(Note note) { Notes.Remove(note); }


		/// <summary>
		/// Plays all the notes contained by the stave.
		/// Calls the specific Event "PlayList"
		/// </summary>
		public void PlayMusic() {
			isPlaying = true;
			RewindMusic();

            String path = System.Environment.CurrentDirectory;
            Console.WriteLine(path);
            path = path.Replace(@"\bin\x86\Debug", @"\Resources");
            path = path.Replace(@"\bin\x86\Release", @"\Resources");

            MediaPlayer.Stop();
            Uri uri = new Uri(path + @"\Tracks\Iron.wma");
            Song song = Song.FromUri("Iron", uri);
            MediaPlayer.Volume = 100;
            MediaPlayer.Play(song);

			Timer.Interval = TimeSpan.FromMilliseconds(500);//(30000 / CurrentInstrument.Bpm);
			Timer.Tick += new EventHandler(PlayList);
			Timer.Start();

			foreach (NoteView nv in NotesView)
			{
				nv.LaunchAnimation();
			}
		}

		/// <summary>
		/// Get the total time of the played stave
		/// </summary>
		/// <returns>The total time</returns>
		public int GetTotalTime() {
			int time = 1000;
			if (Notes.Count != 0)
				time = (Notes.Last().Position + 4) * 500;//(30000 / CurrentInstrument.Bpm);
			return time;
		}

		/// <summary>
		/// Event triggered when the play button is touched.
		/// Play all the notes on the current stave
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void PlayList(object source, EventArgs e) {
			bool play = true;

			if (PositionNote <= MaxPosition + 4)
			{
				while (play && (IteratorNotes < Notes.Count))
				{
					if (Notes[IteratorNotes].Position == PositionNote)
					{
						//CurrentInstrument.PlayNote(Notes[IteratorNotes]);
						IteratorNotes++;
					}
					else
						play = false;

				}
				PositionNote++;
			}
			else
			{
				RewindMusic();
				foreach (NoteView nv in NotesView)
				{
					nv.LaunchAnimation();
				}
			}
		}

		/// <summary>
		/// Stops the music currently playing.
		/// </summary>
		public void StopMusic() {
			isPlaying = false;
            MediaPlayer.Stop();
			Timer.Stop();
			Timer.Tick -= new EventHandler(PlayList);
			RewindMusic();

			foreach (NoteView nv in NotesView)
			{
				nv.StopAnimation();
			}
		}


		/// <summary>
		/// Rewind music at begining without stoping
		/// </summary>
		public void RewindMusic() {
			PositionNote = -3;
			IteratorNotes = 0;
		}


		public bool IsPlaying()
		{
			return isPlaying;
		}


		/// <summary>
		/// Get pitch of the currently played note
		/// </summary>
		public string GetCurrentNote()
		{
			if (!isPlaying || (IteratorNotes >= Notes.Count))
				return "";

			return Notes[IteratorNotes].Pitch;
		}
	}
}
