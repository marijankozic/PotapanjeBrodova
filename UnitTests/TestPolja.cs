using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PotapanjeBrodova;

namespace UnitTests
{
    [TestClass]
    public class TestPolja
    {
        [TestMethod]
        public void Polje_SvojstvaRedakStupacJednakaSuVrijednostimaZadanimKonstruktorom() {
            Polje p = new Polje(1, 2);
            Assert.AreEqual(1, p.Redak);
            Assert.AreEqual(2, p.Stupac);
        }
    }
}
