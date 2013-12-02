using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Surface.Presentation.Controls;

namespace Proto1 {
	public class NoteView {
		/// <summary>
        /// Property.
        /// The Note in the NoteViewModel
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Property.
        /// The parent ScatterView.
        /// </summary>
        public ScatterView ParentSV { get; set; }

        /// <summary>
        /// Property.
        /// The ScatterViewItem containing the note.
        /// </summary>
        public ScatterViewItem SVItem { get; set; }

        /// <summary>
        /// Parameter.
        /// The NoteAnimation item handling all animations for the note.
        /// </summary>
        //public NoteAnimation Animation { get; set; }

        /// <summary>
        /// Defines if the note is being moved by user
        /// </summary>
        public bool Picked { get; set; }


		private Storyboard stb; // Manager of note animation



        
        /// <summary>
        /// Constructor
        /// Create un NoteViewModel
        /// </summary>
        /// <param name="n">The note in the NoteViewModel</param>
        /// <param name="sv">The ScatterView Parent</param>
        public NoteView(Note n, ScatterView sv)
        {
			Note = n;
			ParentSV = sv;
			SurfaceWindow1.melody.NotesView.Add(this);

			stb = new Storyboard();
			SVItem = new ScatterViewItem();
			StopAnimation(); // Set initial position

            SetStyle();

			ParentSV.Items.Add(SVItem);
        }


        /// <summary>
        /// Set the Style of the bubbleImage
        /// </summary>
        public void SetStyle()
		{
			SVItem.Background = Brushes.Transparent;
			SVItem.CanScale = false;
			SVItem.HorizontalAlignment = HorizontalAlignment.Center;
			SVItem.CanRotate = false;
			SVItem.HorizontalAlignment = HorizontalAlignment.Center;

            Image bubbleImage = new Image();
			bubbleImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri(@"..\Resources\UI\Notes\crotchet.png", UriKind.Relative)));
			bubbleImage.SetValue(Image.WidthProperty, 130.0);
			bubbleImage.SetValue(Image.HeightProperty, 130.0);
			bubbleImage.SetValue(Image.IsHitTestVisibleProperty, false);

			Ellipse touchZone = new Ellipse();
			//touchZone.SetValue(Ellipse.OpacityProperty, 0.3);
			touchZone.SetValue(Ellipse.FillProperty, Brushes.Transparent);
			touchZone.SetValue(Ellipse.WidthProperty, 20.0);
			touchZone.SetValue(Ellipse.HeightProperty, 20.0);

			Grid grid = new Grid();
			grid.Children.Add(bubbleImage);
			grid.Children.Add(touchZone);

			SVItem.Content = grid;
        }


		/// <summary>
		/// Play reading animation (slide from right to left)
		/// </summary>
		public void LaunchAnimation()
		{
			//stb.Stop(ParentSV);

			Point startPoint = new Point(400, (Note.PitchToInt() + 5) * 12);
			//Point startPoint = new Point(Note.Position * 60 + 150, (Note.PitchToInt() + 5) * 12);
			Point endPoint = new Point(-10, startPoint.Y);
			SVItem.Center = startPoint;

			PointAnimation moveCenter = new PointAnimation();
			moveCenter.From = startPoint;
			moveCenter.To = endPoint;
			moveCenter.BeginTime = TimeSpan.FromSeconds((float)(Note.Position) / 2);
			moveCenter.Duration = new Duration(TimeSpan.FromSeconds(2.5));
			//moveCenter.Duration = new Duration(TimeSpan.FromSeconds((float)(Note.Position) + 1));
			moveCenter.FillBehavior = FillBehavior.Stop;

			stb.Children.Add(moveCenter);

			Storyboard.SetTarget(moveCenter, SVItem);
			Storyboard.SetTargetProperty(moveCenter, new PropertyPath(ScatterViewItem.CenterProperty));

			stb.Begin(ParentSV);
			SVItem.Center = new Point(400, startPoint.Y);
		}


		/// <summary>
		/// Stop reading animation
		/// </summary>
		public void StopAnimation()
		{
			stb.Stop(ParentSV);
            SVItem.Center = new Point(Note.Position * 22, (Note.PitchToInt() + 5) * 12);
		}

	}
}
