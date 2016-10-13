using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Z80Processor
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RegisterMap
    {
        [FieldOffset(0)] public byte A;
        [FieldOffset(1)] public byte F;
        [FieldOffset(0)] public Int16 AF;

        [FieldOffset(2)] public byte B;
        [FieldOffset(3)] public byte C;
        [FieldOffset(2)] public Int16 BC;

        [FieldOffset(4)] public byte D;
        [FieldOffset(5)] public byte E;
        [FieldOffset(4)] public Int16 DE;

        [FieldOffset(6)] public byte H;
        [FieldOffset(7)] public byte L;
        [FieldOffset(6)] public Int16 HL;


        [FieldOffset(8)] public Int16 PC;
    }

    public class Z80Core
    {
        private RegisterMap registers = new RegisterMap();

        private byte[] memory = new byte[0xFFFF];
        public byte[] Memory { get { return memory; } }

        public RegisterMap Registers
        {
            get { return registers; }
        }

        public void Start(Int16 startAddress = 0x0, int stopOn = 0xFFFF)
        {
            registers.PC = startAddress;
            while (true)
            {
                byte commandCode = memory[registers.PC];
                switch (commandCode)
                {
                    case 0x00: break; //nop
                    case 0x01: registers.B = memory[++registers.PC]; registers.C = memory[++registers.PC]; break; //ld bc,**
                    case 0x04: registers.B++; break; //inc b
                    case 0x05: registers.B--; break; //dec b
                    case 0x06: registers.B = memory[++registers.PC]; break; //ld b,*
                    case 0x3E: registers.A = memory[++registers.PC]; break; //ld a,*
                    case 0x47: registers.B = registers.A; break; //ld b,a

                }
                registers.PC++;
                if (registers.PC == stopOn)
                    return;
            }
        }
    }
}
