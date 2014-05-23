using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public class Player
    {
        private List<Tuple<double, double, Note>>.Enumerator enumerator;
        // Tolerance, in milliseconds, for which a pressed note is still considered valid
        public const double TIMING_TOLERANCE = 500;

        public SheetMusic SheetMusic { get; set; }
        public Game CurrentGame { get; set; }
        public Boolean Ready { get; set; }
        public Instrument Instrument { get; set; }
        public Difficulty InstrumentDifficulty { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Score { get; set; }
        public int Combo { get; set; }
        public int MaxCombo { get; set; }
        public Timer Timer { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public event TickHandler Tick;
        public class NoteTicked : EventArgs
        {
            public Note Note { get; set; }
        }
        public delegate void TickHandler(Player p, NoteTicked nt);

        public Player() 
        {
            this.Difficulty = Difficulty.Undefined;
            this.Instrument = Instrument.Undefined;
            this.InstrumentDifficulty = Difficulty.Undefined;
            this.Ready = false;
        }

        /// <summary>
        /// Inform the player a song is selected, a game is created 
        /// and it can chose an instrument and a difficulty
        /// </summary>
        public void InformNewGame()
        {
            this.CurrentGame = GameMaster.Instance.Game;
            this.Ready = false;
        }

        /// <summary>
        /// The player has chosen instrument and difficulty
        /// Building sheet must be done here and not in the VM
        /// </summary>
        public void IMReady()
        {
            // FIXME : Uncomment these lines once everything is correctly instanciated
            // this.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, Instrument, Difficulty);
            if (CurrentGame.IsPlaying)
            {
                this.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, Instrument, InstrumentDifficulty);
                GameMaster.Instance.Game.AddPlayerInGame(this);
            }
            this.Ready = true;
        }

        /// <summary>
        /// The player has something to do before launching the game
        /// </summary>
        public void NotReadyAnymore()
        {
            this.Ready = false;
        }

        /*
         * EXAMPLE OF HOW TO USE NOTETICKED EVENT
        Player p;
        // Call that line before launching p.ReadSheet()
        p.Tick += new Player.TickHandler(OnTickedNote);
        // This is the method that will be run when the TickedNote Event is launched.
        // The method must return void and have a Player and a Player.NoteTicked arguments
        public void OnTickedNote(Player p, Player.NoteTicked nt)
        {
            // Do something, for example :
            Console.WriteLine(nt.Note.Length.ToString() + " " + nt.Note.Accidental.ToString() + " " + nt.Note.Height.ToString());
        }
        // Or in a more compact manner :
        p.Tick += delegate(Player sender, Player.NoteTicked nt)
        {
            // Do something
        };
        */

        /// <summary>
        /// Launch the sheet reading of the player
        /// </summary>
        /// <param name="IsAnEnumeratorGiven">Is there a given enumerator in parameters ?</param>
        /// <param name="e">Enumerator given when adding a player in a launched game</param>
        public void ReadSheet(bool IsAnEnumeratorGiven = false, List<Tuple<double, double, Note>>.Enumerator e = new List<Tuple<double, double, Note>>.Enumerator())
        {
            this.enumerator = this.SheetMusic.Notes.GetEnumerator();
            if (IsAnEnumeratorGiven)
            {
                this.enumerator = e;
            }

            this.Timer = new Timer(2000);
            this.Timer.AutoReset = false;
            this.Timer.Elapsed += new ElapsedEventHandler(OnTimerTicked);
            this.Stopwatch = new Stopwatch();
            this.Timer.Start();
        }

        /// <summary>
        /// Launch an event with the note to play when it has to be displayed
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimerTicked(object source, ElapsedEventArgs e)
        {
            this.Stopwatch.Start();

            double myTime = 0;
            if (enumerator.Current == null)
            {
                enumerator.MoveNext();
            }
            if (Tick != null)
            {
                NoteTicked nt = new NoteTicked();
                nt.Note = new Note(this.enumerator.Current.Item3);
                this.Stopwatch.Stop();
                nt.Note.StartPlaying(this.enumerator.Current.Item2 - this.Stopwatch.ElapsedMilliseconds + TIMING_TOLERANCE);
                this.Stopwatch.Start();
                //nt.Note.Tick += new Note.TickHandler(() => { this.Combo = 0; });
                Tick(this, nt);
            }
            myTime = this.enumerator.Current.Item1;
            if (enumerator.MoveNext())
            {
                this.Timer.Interval = this.enumerator.Current.Item1 - myTime;
                this.Timer.Start();
            }
            // End
            else
            {
                this.Timer.Close();
                this.Stopwatch.Reset();
            }
        }

        /// <summary>
        /// Update Player score when a note is played
        /// </summary>
        /// <param name="difference">The difference, in seconds, between when the note 
        /// should have been played and when it was actually played.</param>
        public void NoteScored(double difference)
        // The figures may have to be changed
        {
            // Multipliers
            int aScore = 10;
            int bScore = 5;
            int cScore = 1;
            int nope = 0;

            // Delays
            double aDelay = 0.25;
            double bDelay = 0.5;
            double cDelay = 1.0;

            if (difference <= aDelay)
            {
                this.Score += aScore;
            }
            else if (difference <= bDelay)
            {
                this.Score += bScore;
            }
            else if (difference <= cDelay)
            {
                this.Score += cScore;
            }
            else
            {
                this.Score += nope;
            }

        }

        /// <summary>
        /// Increments Player's combo info
        /// </summary>
        public void ScoreCombo()
        {
            this.Combo++;
            this.MaxCombo = Math.Max(this.MaxCombo, this.Combo);
        }

        /// <summary>
        /// Set all the player's scores to 0
        /// </summary>
        public void ResetScores()
        {
            this.Score = 0;
            this.Combo = 0;
            this.MaxCombo = 0;
        }
    }
}
