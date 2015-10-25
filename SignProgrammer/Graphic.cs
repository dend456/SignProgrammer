using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SignProgrammer
{
    public class Graphic : SignEffect
    {
        /* valid colors
        B - black
        G - green
        R - red
        Y - orange
        */

        public const int WIDTH = 18;
        public const int HEIGHT = 7;

        public string Data { get; private set; }

        public Graphic(string name, string displayText, string data) : base(name, displayText, null, SignEffectType.Graphic)
        {
            if (data.Length != WIDTH * HEIGHT)
            {
                throw new FormatException("Graphic data is not the correct size. Must be 18x7.");
            }
            Data = data;
        }

        public static Graphic LoadFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return new Graphic(lines[0], lines[1], lines[2]);
        }
    }
}
