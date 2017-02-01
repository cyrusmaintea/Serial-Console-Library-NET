using System;
using System.IO;
using System.Text;
using System.IO.Ports;

namespace SIOLib
{
    public class SIOFile
    {
        public byte[] ReadByteArrayFromFile(string fileName)
        {
            byte[] buff = null;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileName).Length;
                buff = br.ReadBytes((int)numBytes);   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return buff;
        }

        public void SendTextFile(SerialPort port, string FileName)
        {
            try
            {
                port.Write(File.OpenText(FileName).ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            } 
        }

        public void SendBinaryFile(SerialPort port, string FileName)
        {
            try
            {
                using (FileStream fs = File.OpenRead(FileName))
                    port.Write((new BinaryReader(fs)).ReadBytes((int)fs.Length), 0, (int)fs.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}