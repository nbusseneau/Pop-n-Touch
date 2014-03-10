using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace Proto1
{
	/// <summary>
	/// Interaction logic for SurfaceWindow1.xaml
	/// </summary>
	public partial class SurfaceWindow1 : SurfaceWindow
	{
		/// <summary>
		/// Enum of possible Game State/UIScreen
		/// </summary>
		private enum GameState
		{
			Idle, // Only before initialization
			Start,
			ChooseSong,
			ChooseInstruments,
			PlaySong,
			PauseSong,
			EndSong
		}
		private GameState currentState;

		private Instrument instrument;
		public static Melody melody;


		/// <summary>
		/// Default constructor.
		/// </summary>
		public SurfaceWindow1()
		{
			InitializeComponent();

			SwitchGameState(GameState.Start);
			Storyboard animBtnStart = (Storyboard) FindResource("animBtnStart");
			animBtnStart.Begin();

			instrument = new Instrument(InstrumentType.piano);
			melody = new Melody(instrument);

			// Add handlers for window availability events
			AddWindowAvailabilityHandlers();
		}


		/// <summary>
		/// Logic of transition from a game state (UI screen) to another
		/// </summary>
		/// <param name="newState"></param>
		private void SwitchGameState(GameState newState)
		{
			if (newState == currentState)
				return;
			currentState = newState;

			btnStart.Visibility = System.Windows.Visibility.Hidden;
			btnSong1.Visibility = System.Windows.Visibility.Hidden;
			btnSong2.Visibility = System.Windows.Visibility.Hidden;
			btnSong3.Visibility = System.Windows.Visibility.Hidden;
			btnSong4.Visibility = System.Windows.Visibility.Hidden;
			btnSong5.Visibility = System.Windows.Visibility.Hidden;
			btnPlayPause.Visibility = System.Windows.Visibility.Hidden;
			btnAddTab.Visibility = System.Windows.Visibility.Hidden;
			btnEraseAll.Visibility = System.Windows.Visibility.Hidden;

			switch (currentState)
			{
				case GameState.Start:
					btnStart.Visibility = System.Windows.Visibility.Visible;
					startmenu.Visibility = System.Windows.Visibility.Hidden;
                    menu.Visibility = System.Windows.Visibility.Hidden;
                    btnSong1.Visibility = System.Windows.Visibility.Hidden;
					btnSong2.Visibility = System.Windows.Visibility.Hidden;
					btnSong3.Visibility = System.Windows.Visibility.Hidden;
					btnSong4.Visibility = System.Windows.Visibility.Hidden;
					btnSong5.Visibility = System.Windows.Visibility.Hidden;
                    break;
				case GameState.ChooseSong:
                    startmenu.Visibility = System.Windows.Visibility.Visible;
                    menu.Visibility = System.Windows.Visibility.Visible;
					btnSong1.Visibility = System.Windows.Visibility.Visible;
					btnSong2.Visibility = System.Windows.Visibility.Visible;
					btnSong3.Visibility = System.Windows.Visibility.Visible;
					btnSong4.Visibility = System.Windows.Visibility.Visible;
					btnSong5.Visibility = System.Windows.Visibility.Visible;
					break;
				case GameState.ChooseInstruments:
					btnPlayPause.Visibility = System.Windows.Visibility.Visible;
					btnAddTab.Visibility = System.Windows.Visibility.Visible;
					break;
				case GameState.PlaySong:
					btnPlayPause.Visibility = System.Windows.Visibility.Visible;
					btnAddTab.Visibility = System.Windows.Visibility.Visible;
					btnEraseAll.Visibility = System.Windows.Visibility.Visible;
					break;
			}
		}


#region WindowDefaultFunctions

		/// <summary>
		/// Occurs when the window is about to close. 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			// Remove handlers for window availability events
			RemoveWindowAvailabilityHandlers();
		}

		/// <summary>
		/// Adds handlers for window availability events.
		/// </summary>
		private void AddWindowAvailabilityHandlers()
		{
			// Subscribe to surface window availability events
			ApplicationServices.WindowInteractive += OnWindowInteractive;
			ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
			ApplicationServices.WindowUnavailable += OnWindowUnavailable;
		}

		/// <summary>
		/// Removes handlers for window availability events.
		/// </summary>
		private void RemoveWindowAvailabilityHandlers()
		{
			// Unsubscribe from surface window availability events
			ApplicationServices.WindowInteractive -= OnWindowInteractive;
			ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
			ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
		}

		/// <summary>
		/// This is called when the user can interact with the application's window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWindowInteractive(object sender, EventArgs e)
		{
			//TODO: enable audio, animations here
		}

		/// <summary>
		/// This is called when the user can see but not interact with the application's window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWindowNoninteractive(object sender, EventArgs e)
		{
			//TODO: Disable audio here if it is enabled

			//TODO: optionally enable animations here
		}

		/// <summary>
		/// This is called when the application's window is not visible or interactive.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnWindowUnavailable(object sender, EventArgs e)
		{
			//TODO: disable audio, animations here
		}
#endregion

#region ButtonEvents
		private void Button_Start_Click(object sender, RoutedEventArgs e)
		{
			SwitchGameState(GameState.ChooseSong);
		}

		private void Button_ChooseSong_Click(object sender, RoutedEventArgs e)
		{
			SwitchGameState(GameState.ChooseInstruments);
		}

		private void Button_AddTab_Click(object sender, RoutedEventArgs e) {
			Tab t = new Tab();
			scatterViewTabsContainer.Items.Add(t);
		}

		private void Button_EraseAll_Click(object sender, RoutedEventArgs e)
		{
			SwitchGameState(GameState.Start);
			melody.Notes.Clear();
			melody = new Melody(instrument);

			scatterViewTabsContainer.Items.Clear();
		}

		public void Button_PlayStop_Checked(object sender, RoutedEventArgs e) {
			SwitchGameState(GameState.PlaySong);
			melody.PlayMusic();
		}

		public void Button_PlayStop_Unchecked(object sender, RoutedEventArgs e) {
			SwitchGameState(GameState.ChooseInstruments);
			melody.StopMusic();
		}

#endregion

	}
}