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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Media.Animation;

namespace PopNTouch2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Only used for complex start screen animation
    /// Source : http://joshsmithonwpf.wordpress.com/2007/08/13/animating-text-in-wpf/
    /// </summary>
    public partial class MainWindow : SurfaceWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the TextBlock.
        /// </summary>
        void StartTextAnimations(object sender, RoutedEventArgs e)
        {
            this.titleTextBlock.TextEffects = new TextEffectCollection();

            Storyboard storyBoardWave = new Storyboard();

            Storyboard storyBoardRotation = new Storyboard();
            storyBoardRotation.RepeatBehavior = RepeatBehavior.Forever;
            storyBoardRotation.AutoReverse = true;

            for (int i = 0; i < this.titleTextBlock.Text.Length; ++i)
            {
                // Add a TextEffect for the current character
                // so that it can be animated.
                this.AddTextEffectForCharacter(i);

                // Add an animation which makes the 
                // character float up and down.
                this.AddWaveAnimation(storyBoardWave, i);

                // Add an animation which makes the character rotate.
                this.AddRotationAnimation(storyBoardRotation, i);
            }

            // Add the animation which creates 
            // a pause between rotations.
            Timeline pause =
                this.FindResource("CharacterRotationPauseAnimation")
                as Timeline;

            storyBoardRotation.Children.Add(pause);

            // Start the animations 
            storyBoardWave.Begin(this);
            storyBoardRotation.Begin(this);
        }

        /// <summary>
        /// Adds a TextEffect for the specified character 
        /// which contains the transforms necessary for 
        /// animations to be applied.
        /// </summary>
        void AddTextEffectForCharacter(int charIndex)
        {
            TextEffect effect = new TextEffect();

            // Tell the effect which character 
            // it applies to in the text.
            effect.PositionStart = charIndex;
            effect.PositionCount = 1;

            TransformGroup transGrp = new TransformGroup();
            transGrp.Children.Add(new TranslateTransform());
            transGrp.Children.Add(new RotateTransform());
            effect.Transform = transGrp;

            this.titleTextBlock.TextEffects.Add(effect);
        }

        /// <summary>
        /// Adds an animation to the specified character 
        /// in the display text so that it participates 
        /// in the animated 'wave' effect.
        /// </summary>
        void AddWaveAnimation(Storyboard storyBoardLocation, int charIndex)
        {
            DoubleAnimation anim =
                this.FindResource("CharacterWaveAnimation")
                as DoubleAnimation;

            this.SetBeginTime(anim, charIndex);

            string path = String.Format(
                "TextEffects[{0}].Transform.Children[0].Y",
                charIndex);

            PropertyPath propPath = new PropertyPath(path);
            Storyboard.SetTargetProperty(anim, propPath);

            storyBoardLocation.Children.Add(anim);
        }

        /// <summary>
        /// Adds an animation to the specified character 
        /// in the display text so that it participates 
        /// in the animated rotation effect.
        /// </summary>
        void AddRotationAnimation(
            Storyboard storyBoardRotation, int charIndex)
        {
            DoubleAnimation anim =
                this.FindResource("CharacterRotationAnimation")
                as DoubleAnimation;

            this.SetBeginTime(anim, charIndex);

            string path = String.Format(
                "TextEffects[{0}].Transform.Children[1].Angle",
                charIndex);

            PropertyPath propPath = new PropertyPath(path);
            Storyboard.SetTargetProperty(anim, propPath);

            storyBoardRotation.Children.Add(anim);
        }

        void SetBeginTime(Timeline anim, int charIndex)
        {
            double totalMs = anim.Duration.TimeSpan.TotalMilliseconds;
            double offset = totalMs / 10;
            double resolvedOffset = offset * charIndex;
            anim.BeginTime = TimeSpan.FromMilliseconds(resolvedOffset);
        }
    }
}
