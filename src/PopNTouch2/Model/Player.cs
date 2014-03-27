using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace PopNTouch2.Model
{
    public class Player
    {
        /*private GameMaster gameMaster;
        private SheetMusic sheetmusic;
        private Game currentGame;
        private Boolean ready;
        private List<Instrument> availableInstruments;
        private Instrument instrument;
        private Difficulty difficulty; */
        private List<Tuple<double, Note>>.Enumerator enumerator;

        public SheetMusic SheetMusic { get; set; }
        public Game CurrentGame { get; set; }
        public Boolean Ready { get; set; }
        public Instrument Instrument { get; set; }
        public Difficulty Difficulty { get; set; }
        public Timer Timer { get; set; }
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
            this.Ready = false;
        }

        public void InformNewGame()
        {
            this.CurrentGame = GameMaster.Instance.Game;
            this.Ready = false;
        }

        public void IMReady()
        {
            // FIXME : Uncomment these lines once everything is correctly instanciated
            // this.SheetMusic = GameMaster.Instance.SheetBuilder.BuildSheet(GameMaster.Instance.Game.Song, Instrument, Difficulty);
            this.Ready = true;
            // GameMaster.Instance.Ready();
        }

        public void NotReadyAnymore()
        {
            this.Ready = false;
            this.Difficulty = Difficulty.Undefined;
            this.Instrument = Instrument.Undefined;
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

        public void ReadSheet()
        {
            this.enumerator = this.SheetMusic.Notes.GetEnumerator();
            this.Timer = new Timer(this.SheetMusic.TimeRest);
            this.Timer.AutoReset = false;
            this.Timer.Elapsed += new ElapsedEventHandler(OnTimerTicked);
            this.Timer.Start();
        }

        public void OnTimerTicked(object source, ElapsedEventArgs e)
        {
            if (this.enumerator.MoveNext())
            {
                if (Tick != null)
                {
                    NoteTicked nt = new NoteTicked();
                    nt.Note = this.enumerator.Current.Item2;
                    Tick(this, nt);
                }
                this.Timer.Interval = this.enumerator.Current.Item1;
                this.Timer.Start();
            }
        }
    }
}
