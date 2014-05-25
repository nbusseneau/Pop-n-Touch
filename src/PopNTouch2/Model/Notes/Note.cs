using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    /// <summary>
    /// Note class, a state machine used to describe notes read from a SheetMusic
    /// Used to be a flyweight with NoteFactory, before adding the state machine
    /// Could be one with an Abstract Factory
    /// </summary>
    public class Note
    {
        public Length Length { get; private set; }
        public Accidental Accidental { get; private set; }
        public Height Height { get; private set; }
        public NoteState State { get; set; }

        /// <summary>
        /// Timer set up to fire an event if this Note wasn't played on time
        /// </summary>
        public Timer MissTimer { get; set; }
        public event TickHandler Tick;
        public delegate void TickHandler();

        /// <summary>
        /// Creates a new Note of length l, accidental a, height h and in Waiting state
        /// </summary>
        /// <param name="l">Length of the new note</param>
        /// <param name="a">Accidental of the new note</param>
        /// <param name="h">Height of the new note</param>
        public Note(Length l, Accidental a, Height h)
        {
            this.Length = l;
            this.Accidental = a;
            this.Height = h;
            this.State = NoteState.Waiting;
        }

        /// <summary>
        /// Starts playing the current Note, eg changing its State and starting its timer
        /// </summary>
        /// <param name="timerInterval">Time (in milliseconds) after which this Note will switch to Missed state</param>
        public void StartPlaying(double timerInterval)
        {
            this.State = NoteState.Playing;
            this.MissTimer = new Timer(timerInterval);
            this.MissTimer.Elapsed += new ElapsedEventHandler((sender, e) => { this.Miss(); });
            this.MissTimer.Start();
        }

        /// <summary>
        /// Current Note has been hit on time
        /// </summary>
        public void Hit()
        {
            this.State = NoteState.Hit;
            this.Tick();
            this.MissTimer.Close();
        }

        /// <summary>
        /// Current Note has been missed
        /// </summary>
        public void Miss()
        {
            this.State = NoteState.Missed;
            this.Tick();
            this.MissTimer.Close();
        }

        /// <summary>
        /// Current Note goes in Paused state
        /// Stops its Timer and warns any subscribed TickHandler of the change (used to warn NoteVM)
        /// </summary>
        public void Pause()
        {
            this.MissTimer.Stop();
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
            this.MissTimer.Start();
            this.State = NoteState.Waiting;
            this.Tick();
        }
    }
}
