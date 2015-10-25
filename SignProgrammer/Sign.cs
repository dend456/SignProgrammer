using System;
using System.Text;
using System.IO.Ports;
using System.Collections.Generic;
using System.Threading;

namespace SignProgrammer
{
    public class Sign
    {
        /*  <?[A-Z]> commands
        ? =
        B - graphic, 
        C - color
        E - ??
        S - ??
        */

        public string Id { get; set; } = "01";
        public String Port { get; set; } = "COM4";
        public int Baud { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;

        private static List<SignEffect> effects;
        public static List<SignEffect> Effects
        {
            get
            {
                if (effects == null)
                {
                    effects = LoadEffects();
                }
                return effects;
            }
        }

        private string ParseMessage(string msg, string page = "A")
        {
            int currentIndex = 0;
            char currentGraphic = 'A';
            var graphics = new List<Graphic>();

            msg = msg.Trim().Replace("\r\n", " ");

            while (currentIndex >= 0 && currentIndex < msg.Length)
            {
                currentIndex = msg.IndexOf('{', currentIndex);
                if (currentIndex >= 0)
                {
                    int endIndex = msg.IndexOf('}', currentIndex + 1);
                    if (endIndex >= 0)
                    {
                        string sub = msg.Substring(currentIndex, endIndex - currentIndex + 1);
                        SignEffect eff = effects.Find(e => e.Text == sub);
                        if (eff != null)
                        {
                            switch (eff.Type)
                            {
                                case SignEffect.SignEffectType.Graphic:
                                {
                                    Graphic g = eff as Graphic;
                                    graphics.Add(g);
                                    msg = msg.Replace(sub, string.Format("<G{0}>", currentGraphic));
                                    currentGraphic = (char)((int)currentGraphic + 1);
                                    break;
                                }
                                case SignEffect.SignEffectType.Font:
                                case SignEffect.SignEffectType.Color:
                                case SignEffect.SignEffectType.Transition:
                                case SignEffect.SignEffectType.Special:
                                {
                                    msg = msg.Replace(sub, eff.MessageText);
                                    currentIndex = currentIndex + eff.MessageText.Length;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ++currentIndex;
                        }
                    }
                }
            }

            List<string> lines = new List<string>()
            {
                string.Format("<ID{0}><P{1}>{2}", Id, page, msg)
            };

            for (int i = 0; i < graphics.Count; ++i)
            {
                lines.Add(string.Format("<ID{0}><G{1}>{2}", Id, (char)('A' + i), graphics[i].Data)); 
            }

            lines.Add(string.Format(string.Format("<ID{0}><RP{1}\r\n\r\n", Id, page)));
            return string.Join("\r\n", lines);
        }

        public void SendMessage(string msg, string page = "A")
        {
            msg = ParseMessage(msg);
            Console.WriteLine(msg);
            SendBytes(Encoding.ASCII.GetBytes(msg));
        }

        public void SetSpeed(int speed)
        {
            speed = Math.Min(25, speed);
            speed = -Math.Max(0, speed);
            speed += 'Z';
            string msg = string.Format("<ID01><SPD{0}>\r\n", (char)speed);
            SendBytes(Encoding.ASCII.GetBytes(msg));
        }

        public void SendBytes(byte[] bytes)
        {
            SerialPort ser = new SerialPort(Port, Baud, Parity, DataBits, StopBits);
            ser.Open();

            //write 1 byte at a time and sleep or sign won't be able to keep up
            for (int i=0; i < bytes.Length; ++i)
            {
                ser.Write(bytes, i, 1);
                Thread.Sleep(15);
            }
            ser.Close();
        }

        public static List<SignEffect> LoadEffects()
        {
            List<SignEffect> effects = new List<SignEffect>()
            { 
                new SignEffect("Dim Red","{d-red}","<CA>",SignEffect.SignEffectType.Color),
                new SignEffect("Dim Lime","{d-lime}","<CJ>",SignEffect.SignEffectType.Color),

                new SignEffect("Red","{red}","<CB>",SignEffect.SignEffectType.Color),
                new SignEffect("Orange","{orange}","<CD>",SignEffect.SignEffectType.Color),
                new SignEffect("Yellow","{yellow}","<CG>",SignEffect.SignEffectType.Color),
                new SignEffect("Lime","{lime}","<CI>",SignEffect.SignEffectType.Color),
                new SignEffect("Green","{green}","<CM>",SignEffect.SignEffectType.Color),
                new SignEffect("Rainbow","{rainbow}","<CP>",SignEffect.SignEffectType.Color),

                new SignEffect("Light Yellow","{l-yellow}","<CF>",SignEffect.SignEffectType.Color),
                new SignEffect("Light Green","{l-green}","<CN>",SignEffect.SignEffectType.Color),

                new SignEffect("Bright Red","{b-red}","<CC>",SignEffect.SignEffectType.Color),
                new SignEffect("Bright Orange","{b-orange}","<CE>",SignEffect.SignEffectType.Color),
                new SignEffect("Bright Yellow","{b-yellow}","<CH>",SignEffect.SignEffectType.Color),
                new SignEffect("Bright Lime","{b-lime}","<CK>",SignEffect.SignEffectType.Color),
                new SignEffect("Bright Green","{b-green}","<CL>",SignEffect.SignEffectType.Color),
                
                new SignEffect("Green/Red","{green/red}","<CU>",SignEffect.SignEffectType.Color),
                new SignEffect("Red/Green","{red/green}","<CV>",SignEffect.SignEffectType.Color),
                
                new SignEffect("Yellow/Green/Red","{yellow/green/red}","<CO>",SignEffect.SignEffectType.Color),
                new SignEffect("Red/Black/Green","{red/black/green}","<CQ>",SignEffect.SignEffectType.Color),
                new SignEffect("Red/Black/Yellow","{red/black/yellow}","<CR>",SignEffect.SignEffectType.Color),
                new SignEffect("Red/Green/Black","{red/green/black}","<CZ>",SignEffect.SignEffectType.Color),
                new SignEffect("Green/Red/Black","{green/red/black}","<CY>",SignEffect.SignEffectType.Color),
                new SignEffect("Green/Black/Red","{green/black/red}","<CS>",SignEffect.SignEffectType.Color),
                new SignEffect("Green/Black/Yellow","{green/black/yellow}","<CT>",SignEffect.SignEffectType.Color),
                new SignEffect("Orange/Green/Black","{orange/green/black}","<CW>",SignEffect.SignEffectType.Color),
                new SignEffect("Lime/Red/Black","{lime/red/black}","<CX>",SignEffect.SignEffectType.Color)
            };

            
            return effects;
        }

        public static List<string> OpenComPorts()
        {
            List<string> open = new List<string>();
            SerialPort port;
            for (int i = 0; i < 256; ++i)
            {
                try
                {
                    string portName = "COM" + i.ToString();
                    port = new SerialPort(portName);
                    port.Open();
                    port.Close();
                    open.Add(portName);
                }
                catch (Exception)
                {}
            }
            return open;
        }
    }
}
