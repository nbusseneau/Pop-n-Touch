using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proto1 {
	public class Note {
		public int Position { get; set; }

		public string Pitch { get; set; }

		public int Duration { get; set; }

		public NoteView noteView;

		public Note(int pos, string pitch, int duration)
		{
			Position = pos;
			Pitch = pitch;
			Duration = duration;

			foreach (Tab t in Tab.tabs)
			{
				new NoteView(this, t.scatterViewStave);
			}
		}


		public String GetCue() {
			String PitchTmp = Pitch;
			int OctaveTmp = 1; // 1 or 2
			String alteration = "";
			/*if (Sharp) {
				switch (PitchTmp) {
					case "do": alteration = "_d"; break;
					case "re": alteration = "_d"; break;
					case "mi": PitchTmp = "fa"; break;
					case "fa": alteration = "_d"; break;
					case "sol": alteration = "_d"; break;
					case "la": alteration = "_d"; break;
					case "si": PitchTmp = "do"; OctaveTmp++; break;
				}
			}

			if (Flat) {
				switch (PitchTmp) {
					case "do": PitchTmp = "si"; OctaveTmp--; break;
					case "re": PitchTmp = "do"; alteration = "_d"; break;
					case "mi": PitchTmp = "re"; alteration = "_d"; break;
					case "fa": PitchTmp = "mi"; break;
					case "sol": PitchTmp = "fa"; alteration = "_d"; break;
					case "la": PitchTmp = "sol"; alteration = "_d"; break;
					case "si": PitchTmp = "la"; alteration = "_d"; break;
				}
			}*/
			return PitchTmp + "_" + OctaveTmp.ToString() + alteration;
		}


		public int PitchToInt()
		{
			switch (Pitch) {
				case "do": 
					return 6;
				case "re":
					return 5;
				case "mi":
					return 4;
				case "fa":
					return 3;
				case "sol":
					return 2;
				case "la":
					return 1;
				case "si":
					return 0;
			}
			return -2;
		}

		/// <summary>
		/// Getter of duration's enum number
		/// </summary>
		/// <returns>The Duration's Enumation Rank</returns>
		public int GetDuration() {
			return Duration.GetHashCode();
		}
	}
}
