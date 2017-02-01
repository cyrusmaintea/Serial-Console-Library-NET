using System;
using System.Text;
using System.Runtime.InteropServices;

namespace PIOLib
{
    public class PIOManager
    {
        //X86
        [DllImport("INPOUT32.dll")]
        private static extern UInt32 IsInpOutDriverOpen();

        [DllImport("INPOUT32.dll")]
        private static extern void Out32(short PortAddress, short Data);

        [DllImport("INPOUT32.dll")]
        private static extern char Inp32(short PortAddress);

        [DllImport("INPOUT32.dll")]
        private static extern void DlPortWritePortUshort(short PortAddress, ushort Data);

        [DllImport("INPOUT32.dll")]
        private static extern ushort DlPortReadPortUshort(short PortAddress);

        [DllImport("INPOUT32.dll")]
        private static extern void DlPortWritePortUlong(int PortAddress, uint Data);

        [DllImport("INPOUT32.dll")]
        private static extern uint DlPortReadPortUlong(int PortAddress);

        //X64
        [DllImport("INPOUTX64.dll", EntryPoint = "IsInpOutDriverOpen")]
        private static extern UInt32 IsInpOutDriverOpen_x64();

        [DllImport("INPOUTX64.dll", EntryPoint = "Out32")]
        private static extern void Out32_x64(short PortAddress, short Data);

        [DllImport("INPOUTX64.dll", EntryPoint = "Inp32")]
        private static extern char Inp32_x64(short PortAddress);

        [DllImport("INPOUTX64.dll", EntryPoint = "DlPortWritePortUshort")]
        private static extern void DlPortWritePortUshort_x64(short PortAddress, ushort Data);

        [DllImport("INPOUTX64.dll", EntryPoint = "DlPortReadPortUshort")]
        private static extern ushort DlPortReadPortUshort_x64(short PortAddress);

        [DllImport("INPOUTX64.dll", EntryPoint = "DlPortWritePortUlong")]
        private static extern void DlPortWritePortUlong_x64(int PortAddress, uint Data);

        [DllImport("INPOUTX64.dll", EntryPoint = "DlPortReadPortUlong")]
        private static extern uint DlPortReadPortUlong_x64(int PortAddress);

        [DllImport("INPOUTX64.dll", EntryPoint = "GetPhysLong")]
        private static extern bool GetPhysLong_x64(ref int PortAddress, ref uint Data);

        [DllImport("INPOUTX64.dll", EntryPoint = "SetPhysLong")]
        private static extern bool SetPhysLong_x64(ref int PortAddress, ref uint Data);

        private bool _X64;
        private short _PortAddress; //53264 for D010

        public PIOManager(short PortAddress)
        {
            _X64 = false;
            _PortAddress = PortAddress;

            try
            {
                uint nResult = 0;
                try
                {
                    nResult = IsInpOutDriverOpen();
                }
                catch (BadImageFormatException)
                {
                    nResult = IsInpOutDriverOpen_x64();
                    if (nResult != 0)
                        _X64 = true;
                }
                if (nResult == 0)
                {
                    throw new ArgumentException("Unable to open InpOut driver");
                }
            }
            catch (DllNotFoundException)
            {
                throw new ArgumentException("Unable to find InpOut dll");
            }
        }

        public void Write(short Data)
        {
            if (_X64)
            {
                Out32_x64(_PortAddress, Data);
            }
            else
            {
                Out32(_PortAddress, Data);
            }
        }

        public byte Read()
        {
            if (_X64)
            {
                return (byte)Inp32_x64(_PortAddress);
            }
            else
            {
                return (byte)Inp32(_PortAddress);
            }
        }
    }
}
