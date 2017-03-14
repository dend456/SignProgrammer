using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SignProgrammer.Model
{
    public class PLSign : SerialSign
    {
        /*  <?[A-Z]> commands
        ? =
        B - graphic, 
        C - color
        E - european font
        F - transition / special
        S - font
        */

        
        
        protected override string ParseMessage(string msg, string page)
        {
            int currentIndex = 0;
            char currentGraphic = 'A';
            var graphics = new List<Graphic>();

            msg = msg.Replace("\r\n", " ");

            while (currentIndex >= 0 && currentIndex < msg.Length)
            {
                currentIndex = msg.IndexOf('{', currentIndex);
                if (currentIndex >= 0)
                {
                    int endIndex = msg.IndexOf('}', currentIndex + 1);
                    if (endIndex >= 0)
                    {
                        string sub = msg.Substring(currentIndex, endIndex - currentIndex + 1);
                        SignEffect eff = null;
                        foreach (var kv in Effects)
                        {
                            eff = kv.Value.Find(e => e.Text == sub);
                            if (eff != null)
                            {
                                break;
                            }
                        }
                        if (eff != null)
                        {
                            switch (eff.Type)
                            {
                                case SignEffect.SignEffectType.Graphic:
                                    {
                                        Graphic g = eff as Graphic;
                                        graphics.Add(g);
                                        msg = msg.Replace(sub, string.Format("<B{0}>", currentGraphic));
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
                    else
                    {
                        ++currentIndex;
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

            lines.Add(string.Format(string.Format("<ID{0}><RP{1}>\r\n\r\n", Id, page)));
            return string.Join("\r\n", lines);
        }

        public override bool SetSpeed(int speed)
        {
            speed = Math.Min(25, speed);
            speed = -Math.Max(0, speed);
            speed += 'Z';
            string msg = string.Format("<ID01><SPD{0}>\r\n", (char)speed);
            return SendBytes(Encoding.ASCII.GetBytes(msg));
        }

        public override void AddGraphic(Graphic g)
        {
            Effects["Graphic"].Add(g);
        }

        public override void RemoveGraphic(Graphic g)
        {
            Effects["Graphic"].Remove(g);
        }

        protected override Dictionary<string, List<SignEffect>> LoadEffects()
        {
            return new Dictionary<string, List<SignEffect>>()
            {
                { "Color", LoadColors() },
                { "Graphic", LoadGraphics() },
                { "Font", LoadFont() },
                { "Special", LoadSpecial() },
                { "Transition", LoadTransition() }
            };
        }

        #region Effect_Loading
        private static List<SignEffect> LoadColors()
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
        private static List<SignEffect> LoadTransition()
        {
            List<SignEffect> effects = new List<SignEffect>()
            {
                new SignEffect("Open", "{T:open}", "<FB>", SignEffect.SignEffectType.Transition),
                new SignEffect("Cover", "{T:cover}", "<FC>", SignEffect.SignEffectType.Transition),
                new SignEffect("Close Left", "{T:cl}", "<FF>", SignEffect.SignEffectType.Transition),
                new SignEffect("Close Right", "{T:cr}", "<FG>", SignEffect.SignEffectType.Transition),
                new SignEffect("Close Center", "{T:cc}", "<FH>", SignEffect.SignEffectType.Transition),
                new SignEffect("Scroll Up", "{T:su}", "<FI>", SignEffect.SignEffectType.Transition),
                new SignEffect("Scroll Down", "{T:sd}", "<FJ>", SignEffect.SignEffectType.Transition),
                new SignEffect("Overlap", "{T:overlap}", "<FK>", SignEffect.SignEffectType.Transition),
                new SignEffect("Stack", "{T:stack}", "<FL>",  SignEffect.SignEffectType.Transition),
                new SignEffect("Appear", "{T:appear}", "<FQ>", SignEffect.SignEffectType.Transition),
                new SignEffect("Shift (normal)", "{T:shift}", "<FS>", SignEffect.SignEffectType.Transition)
            };

            return effects;
        }
        private static List<SignEffect> LoadSpecial()
        {
            List<SignEffect> effects = new List<SignEffect>()
            {
                new SignEffect("Random", "{S:random}", "<FA>", SignEffect.SignEffectType.Special),
                new SignEffect("Dots", "{S:dots}", "<FR>", SignEffect.SignEffectType.Special),
                new SignEffect("Date", "{S:date}", "<FD>", SignEffect.SignEffectType.Special),
                new SignEffect("Cycle", "{S:cylce}", "<FE>", SignEffect.SignEffectType.Special),
                new SignEffect("Comic 1", "{S:comic1}", "<FM>", SignEffect.SignEffectType.Special),
                new SignEffect("Comic 2", "{S:comic2}", "<FN>", SignEffect.SignEffectType.Special),
                new SignEffect("Beep", "{S:beep}", "<FO>", SignEffect.SignEffectType.Special),
                new SignEffect("Pause (1 sec.)", "{S:pause}", "<FP>", SignEffect.SignEffectType.Special),
                new SignEffect("Time", "{S:time}", "<FT>", SignEffect.SignEffectType.Special),
                new SignEffect("Random Color", "{S:random color}", "<FU>", SignEffect.SignEffectType.Special),
                new SignEffect("Thank You", "{S:thanks}", "<FV>", SignEffect.SignEffectType.Special),
                new SignEffect("Welcome", "{S:welcome}", "<FW>", SignEffect.SignEffectType.Special)
            };

            return effects;
        }
        private static List<SignEffect> LoadFont()
        {
            List<SignEffect> effects = new List<SignEffect>()
            {
                new SignEffect("4x7", "{F:4x7}", "<SJ>", SignEffect.SignEffectType.Font),
                new SignEffect("5x7", "{F:5x7}", "<SI>", SignEffect.SignEffectType.Font),

                new SignEffect("Normal", "{F:norm}", "<SA>", SignEffect.SignEffectType.Font),
                new SignEffect("Bold", "{F:bold}", "<SB>", SignEffect.SignEffectType.Font),
                new SignEffect("Italic", "{F:italic}", "<SC>", SignEffect.SignEffectType.Font),
                new SignEffect("Bold/Italic", "{F:B/I}", "<SD>", SignEffect.SignEffectType.Font),

                new SignEffect("Flash Normal", "{F:flash}", "<SE>", SignEffect.SignEffectType.Font),
                new SignEffect("Flash Bold", "{F:flash bold}", "<SF>", SignEffect.SignEffectType.Font),
                new SignEffect("Flash Italic", "{F:flash italic}", "<SG>", SignEffect.SignEffectType.Font),
                new SignEffect("Flash Bold Italic", "{F:flash B/I}", "<SH>", SignEffect.SignEffectType.Font),
            };

            return effects;
        }
        private static List<SignEffect> LoadGraphics()
        {
            var effects = new List<SignEffect>();
            string[] files;
            try
            {
                files = Directory.GetFiles(".\\graphics");
            }
            catch (Exception e)
            {
                return effects;
            }

            foreach (var f in files)
            {
                try
                {
                    effects.Add(Graphic.LoadFromFile(f));
                }
                catch (Exception e)
                {
                }
            }

            return effects;
        }
        #endregion
    }
}
