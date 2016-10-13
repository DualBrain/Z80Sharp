using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z80Processor;

namespace Z80CoreTests
{
    [TestFixture]
    public class Z80CoreTests
    {
        private Z80Core sut;
        private void LoadMemoryAndRun(byte[] memoryToLoad)
        {
            Array.Copy(memoryToLoad, sut.Memory, memoryToLoad.Length);
            sut.Start(stopOn: 100);
        }

        [SetUp]
        public void Setup()
        {
            sut = new Z80Core();
        }


        [Test]
        public void TestNOP()
        {
            var memoryToLoad = new byte[] { 0x00 };
            Array.Copy(memoryToLoad, sut.Memory, memoryToLoad.Length);
            sut.Start(stopOn: 100);
            Assert.AreEqual(100, sut.Registers.PC);
        }

        [Test]
        public void TestIncB()
        {
            LoadMemoryAndRun(new byte[] { 0x04 });
            Assert.AreEqual(1, sut.Registers.B);
        }

        [Test]
        public void TestDecB()
        {
            LoadMemoryAndRun(new byte[] { 0x05 });
            Assert.AreEqual(0xFF, sut.Registers.B);
        }
    }
}
