using GalaSoft.MvvmLight;
using SignProgrammer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SignProgrammer.ViewModel
{
    public class GraphicEditorVM : ViewModelBase
    {
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
                var graphics = currentSign.Effects["Graphic"];
                if (graphics.Count > 0)
                {
                    CurrentGraphic = (Graphic)graphics[0];
                }
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
    }
}
