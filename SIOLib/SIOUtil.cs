using System;
using System.Diagnostics;
using System.Text;

namespace SIOLib
{
    public class SIOUtil
    {
        public byte[] ToByteArray(string value)
        {
            char[] charArr = value.ToCharArray();
            byte[] bytes = new byte[charArr.Length];
            for (int i = 0; i < charArr.Length; i++)
            {
                byte current = Convert.ToByte(charArr[i]);
                bytes[i] = current;
            }
            return bytes;
        }

        public char[] ToCharArray(string value)
        {
            char[] charArr = value.ToCharArray();
            return charArr;
        }

        public string ASCIIStrToHex(String asciiString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in asciiString)
                sb.AppendFormat("{0:X2} ", (int)c);
            return sb.ToString().Trim();
        }

        public string HexStrToASCII(String hexString)
        {
            string ascii = string.Empty;
            for (int i = 0; i < hexString.Length; i += 2)
            {
                String hs = string.Empty;
                hs = hexString.Substring(i, 2);
                uint decval = Convert.ToUInt32(hs, 16);
                char character = Convert.ToChar(decval);
                ascii += character;
            }
            return ascii;
        }

        public byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] comBuffer = new byte[msg.Length / 2];

            for (int i = 0; i < msg.Length; i += 2)
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);

            return comBuffer;
        }

        public string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);

            foreach (byte data in comByte)
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));

            return builder.ToString().ToUpper();
        }

        public void Pause(int milli)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; ; i++)
            {
                if (i % 100000 == 0)
                {
                    sw.Stop();
                    if (sw.ElapsedMilliseconds >= milli)
                        break;
                    else
                        sw.Start();
                }
            }
        }
    }
}
