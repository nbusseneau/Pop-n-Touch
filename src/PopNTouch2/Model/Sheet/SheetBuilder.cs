using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PopNTouch2.Model
{
    // Bonuses not implemented
    public class SheetBuilder
    {
        public NoteFactory NoteFactory { get; set; }

        public SheetBuilder()
        {
            this.NoteFactory = new NoteFactory();
        }

        public SheetMusic BuildSheet(Song song, Instrument instr, Difficulty diff)
        {
            SheetMusic sheetMusic = new SheetMusic();

            // Find the file
            string pathOfFileToParse = null;
            try
            { 
                pathOfFileToParse = this.FindFile(song.Name, instr.Name, diff.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new SheetMusic();
            }

            // Read the file and build sheet
            StreamReader sr = File.OpenText(pathOfFileToParse);
            try
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] infos = line.Split(new char[] { ' ' });
                    sheetMusic.Notes.Add(this.NoteFactory.GetNote(ToLength(infos[0]),
                                                                  ToAccidental(infos[1]),
                                                                  ToHeight(infos[2])));
                }
                return sheetMusic;
            }
            // Thrown by ToLength, ToAccidental, ToHeight
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return new SheetMusic();
            }
            finally
            {
                sr.Close();
            }
        }

        /// <exception cref="DirectoryNotFoundException">No directory for that song.</exception>
        /// <exception cref="FileNotFoundException">No file for that instrument and difficulty.</exception>
        public string FindFile(string songName, string instrName, string diffName)
        {
            // Find the song's directory
            string path = "..\\..\\..\\..\\Songs\\" + songName;
            string[] files = Directory.GetFiles(path, "*.song");

            string fileToParse = "";
            char[] separators = new char[] { ' ' };
            foreach (string file in files)
            {
                // Remove path and file extension
                string instrDiff = file.Remove(0, path.Length + 1);
                instrDiff = instrDiff.Replace(".song", "");

                // Find the file with right instrument and difficulty
                string[] split = instrDiff.Split(separators);
                if (split.Length == 2)
                {
                    if (split[0].ToLowerInvariant().Equals(instrName.ToLowerInvariant())
                        && split[1].ToLowerInvariant().Equals(diffName))
                    {
                        fileToParse = file;
                        break;
                    }
                }
            }
            // If something failed
            if (fileToParse.Equals(""))
            {
                throw new FileNotFoundException("No file found for that song, instrument and difficulty.");
            }
            return fileToParse;
        }

        public Length ToLength(string length)
        {
            Length ret;
            switch (length.ToLowerInvariant())
            {
                case "whole":
                    ret = Length.whole;
                    break;
                case "half":
                    ret = Length.half;
                    break;
                case "quarter":
                    ret = Length.quarter;
                    break;
                case"eighth":
                    ret = Length.eighth;
                    break;
                default:
                    throw new ArgumentException("Bad length.");
            }
            return ret;
        }

        public Accidental ToAccidental(string accidental)
        {
            Accidental ret;
            switch (accidental.ToLowerInvariant())
            {
                case "none":
                    ret = Accidental.none;
                    break;
                case "flat":
                    ret = Accidental.flat;
                    break;
                case "sharp":
                    ret = Accidental.sharp;
                    break;
                default:
                    throw new ArgumentException("Bad accidental.");
            }
            return ret;
        }

        public Height ToHeight(string height)
        {
            Height ret;
            switch (height.ToLowerInvariant())
            {
                case "do":
                    ret = Height.@do;
                    break;
                case "re":
                    ret = Height.re;
                    break;
                case "mi":
                    ret = Height.mi;
                    break;
                case "fa":
                    ret = Height.fa;
                    break;
                case "sol":
                    ret = Height.sol;
                    break;
                case "la":
                    ret = Height.la;
                    break;
                case "si":
                    ret = Height.si;
                    break;
                case "rest":
                    ret = Height.rest;
                    break;
                default:
                    throw new ArgumentException("Bad height.");
            }
            return ret;
        }
    }
}
