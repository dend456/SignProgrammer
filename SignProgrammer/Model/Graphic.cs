using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SignProgrammer.Model
{
    public class Graphic : SignEffect
    {
        public const int WIDTH = 18;
        public const int HEIGHT = 7;

        private static readonly Dictionary<char, System.Windows.Media.Color> colors = new Dictionary<char, System.Windows.Media.Color>()
        {
            { 'B', Colors.Black },
            { 'G', Colors.Green },
            { 'R', Colors.Red },
            { 'Y', Colors.Orange }
        };
        public Dictionary<char, System.Windows.Media.Color> ValidColors { get { return colors; } }

        private string data;
        public string Data 
        {
            get
            {
                return data;
            } 
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length != WIDTH * HEIGHT)
                {
                    throw new FormatException("Graphic data is not the correct size. Must be 18x7.");
                }
                data = value;
            } 
        }

        public System.Windows.Media.Color[][] PixelData
        {
            get
            {
                var rows = new System.Windows.Media.Color[HEIGHT][];
                int index = 0;
                for (int y = 0; y < HEIGHT; ++y)
                {
                    var l = new System.Windows.Media.Color[WIDTH];
                    for (int x = 0; x < WIDTH; ++x)
                    {
                        l[x] = colors[data[index++]];
                    }
                    rows[y] = l;
                }
                return rows;
            }
        }
        
        public ImageSource Thumbnail { get; set; }
        public string FilePath { get; set; }
        public string FileName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        public Graphic(string name, string displayText, string path, string data) : base(name, displayText, null, SignEffectType.Graphic)
        {
            if (data.Length != WIDTH * HEIGHT)
            {
                throw new FormatException("Graphic data is not the correct size. Must be 18x7.");
            }
            FilePath = path;
            Data = data;

            byte[] imageData = new byte[WIDTH * HEIGHT * 3];
            int pixelIndex = 0;
            int dataIndex = 0;
            for (int y = 0; y < HEIGHT; ++y)
            {
                for (int x = 0; x < WIDTH; ++x)
                {
                    imageData[pixelIndex++] = colors[data[dataIndex]].R;
                    imageData[pixelIndex++] = colors[data[dataIndex]].G;
                    imageData[pixelIndex++] = colors[data[dataIndex]].B;
                    ++dataIndex;
                }
            }
            PixelFormat pf = PixelFormats.Rgb24;
            BitmapSource bms = BitmapSource.Create(WIDTH, HEIGHT, 96.0, 96.0, pf, null, imageData, WIDTH * 3);
            
            Thumbnail = bms;

        }

        public Graphic() : this("tmp", "{G:tmp}", @".\graphics\tmp.txt", new string('B', 126))
        {
        }

        public bool Save(string path)
        {
            var lines = new List<string>(){ MenuText, Text };
            for (int i = 0; i < HEIGHT * WIDTH; i += WIDTH)
            {
                lines.Add(data.Substring(i, WIDTH));
            }

            try
            {
                File.WriteAllLines(path, lines);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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
