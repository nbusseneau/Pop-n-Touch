using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Surface.Presentation.Controls;

namespace Proto1 {
	/// <summary>
	/// Interaction logic for Tab (Player own view)
	/// </summary>
	public partial class Tab : ScatterViewItem {
		public static List<Tab> tabs; // List of all player tabs

		private DispatcherTimer dispatcherTimer;
		private int pos;


		/// <summary>
		/// Default constructor, initialize with the melody if existing
		/// </summary>
		public Tab() {
			InitializeComponent();

			if (tabs == null)
				tabs = new List<Tab>();
			tabs.Add(this);
			pos = 0;

			if (SurfaceWindow1.melody != null)
			{
				foreach (Note n in SurfaceWindow1.melody.Notes)
				{
					new NoteView(n, scatterViewStave);
				}
			}
		}


		/// <summary>
		/// Check if button pressed is the correct note, then display feedback
		/// </summary>
		/// <param name="pitch"></param>
		private void CheckNotePlayed(String pitch)
		{
			if (pitch == SurfaceWindow1.melody.GetCurrentNote()) // Correct note played
			{
				fdbckCorrect.Visibility = System.Windows.Visibility.Visible;
				fdbckIncorrect.Visibility = System.Windows.Visibility.Hidden;
			}
			else // Bad note
			{
				fdbckCorrect.Visibility = System.Windows.Visibility.Hidden;
				fdbckIncorrect.Visibility = System.Windows.Visibility.Visible;
			}
			dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(HideFeedback);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
			dispatcherTimer.Start();
		}


		private void HideFeedback(object source, EventArgs e)
		{
			dispatcherTimer.Stop();
			fdbckCorrect.Visibility = System.Windows.Visibility.Hidden;
			fdbckIncorrect.Visibility = System.Windows.Visibility.Hidden;
		}


		/// <summary>
		/// Create a new note in the melody at a specific pitch
		/// </summary>
		private void AddNote(String pitch)
		{
			Note newNote = new Note(pos, pitch, 1);
			pos++;
			SurfaceWindow1.melody.AddNote(newNote, newNote.Position);
		}


		#region ButtonsNote_Click

		private void Button_Do_Click(object sender, RoutedEventArgs e) 
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("do");
			else
				AddNote("do");
		}

		private void Button_Re_Click(object sender, RoutedEventArgs e) 
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("re");
			else
				AddNote("re");
		}

		private void Button_Mi_Click(object sender, RoutedEventArgs e) 
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("mi");
			else
				AddNote("mi");
		}

		private void Button_Fa_Click(object sender, RoutedEventArgs e) 
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("fa");
			else
				AddNote("fa");
		}

		private void Button_Sol_Click(object sender, RoutedEventArgs e)
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("sol");
			else
				AddNote("sol");
		}

		private void Button_La_Click(object sender, RoutedEventArgs e)
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("la");
			else
				AddNote("la");
		}

		private void Button_Si_Click(object sender, RoutedEventArgs e)
		{
			if (SurfaceWindow1.melody.IsPlaying())
				CheckNotePlayed("si");
			else
				AddNote("si");
		}

		#endregion
	}
}
