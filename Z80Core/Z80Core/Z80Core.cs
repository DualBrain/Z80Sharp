using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Z80Processor
{
    [StructLayout(LayoutKind.Explicit)]
    public struct TwoByteRegister
    {
        [FieldOffset(0)]
        public byte Byte1;
        [FieldOffset(1)]
        public byte Byte2;
        [FieldOffset(0)]
        public Int16 Int1;
        public override string ToString()
        {
            return string.Format("Byte 1: {0:X}, byte 2: {1:X}", Byte1, Byte2);
        }
    }

    public class Z80Core
    {
        private byte af;
        public byte AF { get { return af; } }

        private TwoByteRegister bc;
        public TwoByteRegister BC { get { return bc; } }

        private Int16 pc;

        private byte[] memory = new byte[0xFFFF];
        public byte[] Memory { get { return memory; } }

        public short PC { get { return pc; } }

        public void Start(Int16 startAddress = 0x0, int stopOn = 0xFFFF)
        {
            pc = startAddress;
            while (true)
            {
                byte commandCode = memory[pc];
                switch (commandCode)
                {
                    case 0x00: break; //nop
                    case 0x01: bc.Byte1 = memory[++pc]; bc.Byte2 = memory[++pc]; break; //ld bc,**
                    case 0x04: bc.Byte1++; break; //inc b
                    case 0x06: bc.Byte1 = memory[++pc]; break; //ld b,*
                }
                pc++;
                if (pc == stopOn)
                    return;
            }
        }
    }
}
