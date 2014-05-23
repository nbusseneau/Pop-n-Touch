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

        public Note(Note n)
        {
            this.Length = n.Length;
            this.Accidental = n.Accidental;
            this.Height = n.Height;
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
            this.Timer.Stop();
        }

        public void Miss()
        {
            this.State = NoteState.Missed;
            this.Tick();
            this.Timer.Stop();
        }
    }
}
