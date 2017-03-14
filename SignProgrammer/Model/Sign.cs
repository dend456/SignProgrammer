using System;
using System.Text;
using System.IO.Ports;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Windows.Media;

namespace SignProgrammer.Model
{
    public abstract class Sign
    {
        public Dictionary<string, List<SignEffect>> Effects { get; protected set; }

        protected abstract string ParseMessage(string msg, string page);
        public abstract bool SendMessage(string msg, string page);
        public abstract bool SetSpeed(int speed);
        public abstract void AddGraphic(Graphic g);
        public abstract void RemoveGraphic(Graphic g);

        protected virtual Dictionary<string, List<SignEffect>> LoadEffects() 
        {
            return new Dictionary<string, List<SignEffect>>()
            {
                { "Color" , new List<SignEffect>() },
                { "Graphic" , new List<SignEffect>() },
                { "Special" , new List<SignEffect>() },
                { "Font" , new List<SignEffect>() },
                { "Transition" , new List<SignEffect>() }
            }; 
        }

        protected Sign()
        {
            Effects = LoadEffects();
        }
    }
}
