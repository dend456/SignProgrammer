using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SignProgrammer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SignProgrammer.ViewModel
{
    public class GraphicEditorVM : ViewModelBase
    {
        private bool isNewGraphic = true;

        public struct Pixel
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Color PixelColor { get; set; }
        }

        private Sign currentSign;
        public Sign CurrentSign 
        { 
            get
            {
                return currentSign;
            }
            set
            {
                currentSign = value;
                CurrentGraphic = new Graphic();
                RaisePropertyChanged("");
            } 
        }

        private Graphic currentGraphic;
        public Graphic CurrentGraphic
        {
            get
            {
                return currentGraphic;
            }
            set
            {
                currentGraphic = value;
                isNewGraphic = false;
                var colors = currentGraphic.PixelData;
                var pixels = new Pixel[colors.Length][];
                int xPos = 0;
                int yPos = 0;
                foreach (var row in colors)
                {
                    var rs = new Pixel[row.Length];
                    xPos = 0;
                    foreach (var c in row)
                    {
                        Pixel p = new Pixel()
                        {
                            X = xPos,
                            Y = yPos,
                            PixelColor = c
                        };
                        rs[xPos] = p;
                        ++xPos;
                    }
                    pixels[yPos] = rs;
                    ++yPos;
                }
                SelectedGraphicPixelData = pixels;
                RaisePropertyChanged("");
            }
        }

        private Color selectedColor;
        public Color SelectedColor
        { 
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                RaisePropertyChanged("");
            } 
        }

        private Pixel[][] selectedGraphicPixelData;
        public Pixel[][] SelectedGraphicPixelData
        {
            get
            {
                return selectedGraphicPixelData;
            }
            set
            {
                selectedGraphicPixelData = value;
                RaisePropertyChanged("");
            }
        }

        public RelayCommand<Pixel> PixelCommand { get; private set; }
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


        public GraphicEditorVM()
        {
            PixelCommand = new RelayCommand<Pixel>((p) =>
            {
                if (SelectedColor.A != 0)
                {
                    var old = SelectedGraphicPixelData;
                    old[p.Y][p.X].PixelColor = SelectedColor;
                    SelectedGraphicPixelData = null;
                    SelectedGraphicPixelData = old;
                }
            });

            NewCommand = new RelayCommand(() =>
            {
                CurrentGraphic = new Graphic();
                isNewGraphic = true;
            });

            SaveCommand = new RelayCommand(() =>
            {
                var colors = currentGraphic.ValidColors;
                var backDict = new Dictionary<Color, char>();
                foreach (var kv in colors)
                {
                    backDict.Add(kv.Value, kv.Key);
                }

                var chars = new List<char>();
                foreach (var row in selectedGraphicPixelData)
                {
                    foreach (var pix in row)
                    {
                        chars.Add(backDict[pix.PixelColor]);
                    }
                }
                currentGraphic.Data = string.Concat(chars);
                currentGraphic.Save();
                if (isNewGraphic)
                {
                    currentSign.AddGraphic(currentGraphic);
                }
                isNewGraphic = false;
                RaisePropertyChanged("");
            });

            ResetCommand = new RelayCommand(() =>
            {
            });

            DeleteCommand = new RelayCommand(() =>
            {
            }); 
        }
    }
}
