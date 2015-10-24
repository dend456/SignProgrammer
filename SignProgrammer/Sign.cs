using System;
using System.Text;
using System.IO.Ports;
using System.Collections.Generic;
using System.Threading;

namespace SignProgrammer
{
    public static class Sign
    {       
        /*  <?[A-Z]> commands
        ? =
        B - graphic, 
        C - color
        E - ??
        S - ??
        */

        public static String Port { get; set; } = "COM4";
        public static int Baud { get; set; } = 9600;
        public static Parity Parity { get; set; } = Parity.None;
        public static int DataBits { get; set; } = 8;
        public static StopBits StopBits { get; set; } = StopBits.One;
        
        public static void SendMessage(string msg, string page = "A")
        {
            msg = msg.Trim();
            msg = msg.Replace("\r\n", " ");
            //string formated = string.Format("<ID01><P{0}>{1}\r\n<ID01><RP{0}>\r\n\r", page, msg);
            Graphic g = Graphic.LoadFromFile("..\\..\\..\\a.txt");
            Graphic g2 = Graphic.LoadFromFile(@"..\..\..\b.txt");
            string formated = string.Format("<ID01><P{0}> A<BA><BB><BA>A \r\n<ID01><GA>{1}\r\n<ID01><GB>{2}\r\n<ID01><RP{0}>\r\n\r\n", page, g2.Data, g.Data);
            Console.WriteLine(formated);
            //string formated = string.Format("<ID01><P{0}>{1}\r\n<ID01><RP{0}>\r\n\r", page, msg);
            SendBytes(Encoding.ASCII.GetBytes(formated));
        }

        public static void SetSpeed(int speed)
        {
            speed = Math.Min(25, speed);
            speed = -Math.Max(0, speed);
            speed += 'Z';
            string msg = string.Format("<ID01><SPD{0}>\r\n", (char)speed);
            SendBytes(Encoding.ASCII.GetBytes(msg));
        }

        public static void SendBytes(byte[] bytes)
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
