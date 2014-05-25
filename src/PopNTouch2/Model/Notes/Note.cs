using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public class Note
    {
        public Length Length { get; private set; }
        public Accidental Accidental { get; private set; }
        public Height Height { get; private set; }
        public NoteState State { get; set; }

        public Timer Timer { get; set; }
        public event TickHandler Tick;
        public delegate void TickHandler();

        public Note(Length l, Accidental a, Height h)
        {
            this.Length = l;
            this.Accidental = a;
            this.Height = h;
            this.State = NoteState.Waiting;
        }

        public void StartPlaying(double timerInterval)
        {
            this.State = NoteState.Playing;
            this.Timer = new Timer(timerInterval);
            this.Timer.Elapsed += new ElapsedEventHandler((sender, e) => { this.Miss(); });
            this.Timer.Start();
        }

        public void Hit()
        {
            this.State = NoteState.Hit;
            this.Tick();
            this.Timer.Close();
        }

        public void Miss()
        {
            this.State = NoteState.Missed;
            this.Tick();
            this.Timer.Close();
        }

        public void Pause()
        {
            this.Timer.Stop();
            this.State = NoteState.Paused;
            this.Tick();
        }

        /// <summary>
        /// Resumes the note
        /// The note's state becomes waiting not to restart the "playing" animation
        /// And to easily exit the "pause" animation
        /// </summary>
        public void Resume()
        {
            this.Timer.Start();
            this.State = NoteState.Resumed;
            this.Tick();
        }
    }
}
