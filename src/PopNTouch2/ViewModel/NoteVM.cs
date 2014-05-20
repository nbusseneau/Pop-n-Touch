using Microsoft.Surface.Presentation.Controls;
using PopNTouch2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PopNTouch2.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        public Note Note { get; set; }

        private Point center;

        public Point Center
        {
            get { return center; }
            set 
            {
                center = value;
                RaisePropertyChanged("Center");
            }
        }

        private Storyboard storyboard;
        public Storyboard Storyboard
        {
            get { return storyboard; }
        }

        public NoteVM(Note note)
        {
            this.Note = note;
        }

        /// <summary>
        /// Creates a Storyboard according to the apparition and play timings in the Model
        /// This could be breaking the MVVM pattern, since animations should be handled by the View, but because of some of its limitations and design choices,
        /// it doesn't seem to be possible
        /// </summary>
        /// <param name="duration"></param>
        public void CreateClassicStoryboard(double duration)
        {
            this.storyboard = new Storyboard();

            PointAnimation centerSlide = new PointAnimation(new Point(560, -10), new Point(30, -10), TimeSpan.FromMilliseconds(duration));
            Storyboard.SetTargetProperty(centerSlide, new PropertyPath(ScatterViewItem.CenterProperty));

            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(ScatterViewItem.OpacityProperty));

            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            fadeOut.BeginTime = TimeSpan.FromMilliseconds(duration - 150);
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath(ScatterViewItem.OpacityProperty));

            this.storyboard.Children.Add(fadeIn);
            this.storyboard.Children.Add(centerSlide);
            this.storyboard.Children.Add(fadeOut);
        }
    }
}
