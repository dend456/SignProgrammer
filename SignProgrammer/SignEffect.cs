using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SignProgrammer
{
    public class SignEffect
    {
        public enum SignEffectType
        {
            Font,
            Graphic,
            Transition,
            Color,
            Special
        }
        
        public string Text { get; protected set; }
        public string MenuText { get; protected set; }
        public string MessageText { get; protected set; }
        public SignEffectType Type { get; protected set; }
        
        public SignEffect(string menuText, string text, string messageText, SignEffectType type)
        {
            Text = text;
            MenuText = menuText;
            MessageText = messageText;
            Type = type;
        }
    }
}
