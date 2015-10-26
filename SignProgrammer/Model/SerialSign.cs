using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignProgrammer.Model
{
    public abstract class SerialSign : Sign
    {
        #region serial_settings
        public string Id { get; set; } = "01";
        public String Port { get; set; } = "COM4";
        public int Baud { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        #endregion

        public override bool SendMessage(string msg, string page)
        {
            msg = ParseMessage(msg, page);
            Console.WriteLine(msg);
            return SendBytes(Encoding.ASCII.GetBytes(msg));
        }

        public bool SendBytes(byte[] bytes)
        {
            try
            {
                using (SerialPort ser = new SerialPort(Port, Baud, Parity, DataBits, StopBits))
                {
                    ser.Open();

                    //write 1 byte at a time and sleep or sign won't be able to keep up
                    for (int i = 0; i < bytes.Length; ++i)
                    {
                        ser.Write(bytes, i, 1);
                        Thread.Sleep(15);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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
                { }
            }
            return open;
        }
    }
}
