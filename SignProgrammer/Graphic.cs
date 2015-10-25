using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace SignProgrammer
{
    public class Graphic : SignEffect
    {
        public const int WIDTH = 18;
        public const int HEIGHT = 7;

        public static List<Tuple<string, Color>> ValidColors { get; } = new List<Tuple<string, Color>>()
        {
            new Tuple<string, Color>("B", Colors.DarkGray),
            new Tuple<string, Color>("G", Colors.Green),
            new Tuple<string, Color>("R", Colors.Red),
            new Tuple<string, Color>("Y", Colors.Orange)
        };

        public string Data { get; private set; }
        public string FilePath { get; private set; }

        public Graphic(string name, string displayText, string path, string data) : base(name, displayText, null, SignEffectType.Graphic)
        {
            if (data.Length != WIDTH * HEIGHT)
            {
                throw new FormatException("Graphic data is not the correct size. Must be 18x7.");
            }
            FilePath = path;
            Data = data;
        }

        public static Graphic LoadFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 3)
            {
                throw new FormatException(string.Format("Failed to load {0}.", path));
            }

            string data = lines[2];
            if (lines.Length > 3)
            {
                for (int i = 3; i < lines.Length; ++i)
                {
                    data += lines[i].Replace("\r\n", "");
                }
            }
            return new Graphic(lines[0], lines[1], path, data);
        }
    }
}
