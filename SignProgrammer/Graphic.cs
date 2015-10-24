using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignProgrammer
{
    public class Graphic
    {
        public const int WIDTH = 18;
        public const int HEIGHT = 7;

        public string Data { get; private set; }

        public Graphic(string data)
        {
            if (data.Length != WIDTH * HEIGHT)
            {
                throw new FormatException("Graphic date is not the correct size. Must be 18x7.");
            }
            Data = data;
        }

        public static Graphic LoadFromFile(string path)
        {
           
           return new Graphic(File.ReadAllText(path).Replace("\r\n",""));
        }
    }
}
