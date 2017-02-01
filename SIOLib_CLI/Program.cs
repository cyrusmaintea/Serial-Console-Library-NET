using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIOLib;

namespace SIOLib_CLI
{
    public class Program
    {
        static SIOManager sioMan = new SIOManager();
        static SIOFile sioFile = new SIOFile();

        static void init()
        {
            sioMan.PortName = "COM6";
            sioMan.BaudRate = "115200";
            sioMan.DataBits = "8";
            sioMan.Parity = "None";
            sioMan.StopBits = "One";

            if (sioMan.OpenPort())
            {
                Console.WriteLine("Opened Serial Port " + sioMan.PortName);
            }
        }

        static void Main(string[] args)
        {
            init();

            Console.WriteLine("Set Transmission Mode: 0 = Text, 1 = Hex");
            string transMode = Console.ReadLine();

            switch (transMode)
            {
                case "0":
                    sioMan.CurrentTransmissionType = SIOManager.TransmissionType.Text;
                    break;

                case "1":
                    sioMan.CurrentTransmissionType = SIOManager.TransmissionType.Hex;
                    break;

                default:
                    sioMan.CurrentTransmissionType = SIOManager.TransmissionType.Read;
                    break;
            }

            string data = string.Empty;
            Console.WriteLine("Input your data:");
            
            do
            {
                data = Console.ReadLine();
                sioMan.WriteData(data);
            } while (data != "quit");

            sioMan.Close();
            Environment.Exit(0);
        }
    }
}
