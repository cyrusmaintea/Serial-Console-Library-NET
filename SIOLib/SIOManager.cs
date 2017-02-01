using System;
using System.Text;
using System.IO.Ports;

namespace SIOLib
{
    public class SIOManager : SIOUtil
    {

        public static SerialPort port = new SerialPort();
        public static string[] names = SerialPort.GetPortNames();

        private static TransmissionType _transType;
        private static string _baudRate = string.Empty;
        private static string _parity = string.Empty;
        private static string _stopBits = string.Empty;
        private static string _dataBits = string.Empty;
        private static string _portName = string.Empty;

        public enum TransmissionType
        {
            Text,
            Hex,
            Read
        }

        public TransmissionType CurrentTransmissionType
        {
            get { return _transType; }
            set { _transType = value; }
        }

        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public string Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        public string StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        public string DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        public bool OpenPort()
        {
            try
            {
                if (port.IsOpen == true)
                {
                    Console.WriteLine("Serial Port is already open!");
                    return false;
                }

                port.BaudRate = int.Parse(_baudRate);
                port.DataBits = int.Parse(_dataBits);
                port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBits);
                port.Parity = (Parity)Enum.Parse(typeof(Parity), _parity);
                port.PortName = _portName;
                port.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void WriteData(string msg)
        {
            switch (CurrentTransmissionType)
            {
                case TransmissionType.Text:
                    if (!(port.IsOpen == true))
                        port.Open();
                    port.WriteLine(msg);
                    Console.WriteLine(msg);
                    break;

                case TransmissionType.Hex:
                    try
                    {
                        byte[] newMsg = HexToByte(msg);
                        port.Write(newMsg, 0, newMsg.Length);
                        Console.WriteLine(ByteToHex(newMsg));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                default:
                    if (!(port.IsOpen == true))
                        port.Open();
                    port.WriteLine(msg);
                    //Console.WriteLine(msg);
                    break;
            }
        }

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (CurrentTransmissionType)
            {
                case TransmissionType.Text:
                    string msg = port.ReadExisting();
                    //Console.WriteLine(msg);
                    break;

                case TransmissionType.Hex:
                    int bytes = port.BytesToRead;
                    byte[] comBuffer = new byte[bytes];
                    port.Read(comBuffer, 0, bytes);
                    //Console.WriteLine(ByteToHex(comBuffer));
                    break;

                case TransmissionType.Read:
                    SerialPort sp = (SerialPort)sender;
                    string str = sp.ReadExisting();
                    Console.WriteLine(str);
                    break;

                default:
                    string def = port.ReadExisting();
                    Console.WriteLine(def);
                    break;
            }
        }

        public void Close()
        {
            try
            {
                port.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void getSerialPortList()
        {
            foreach (string name in names)
            {
                Console.Write(name);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        public SIOManager()
        {
            _baudRate = string.Empty;
            _parity = string.Empty;
            _stopBits = string.Empty;
            _dataBits = string.Empty;
            _portName = string.Empty;

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        public SIOManager(string baud, string par, string sBits, string dBits, string name)
        {
            _baudRate = baud;
            _parity = par;
            _stopBits = sBits;
            _dataBits = dBits;
            _portName = name;

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }
    }
}