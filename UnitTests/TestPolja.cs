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

        [TestMethod]
        public void Polje_PoljaSaIstimStupcemIRetkomSuJednaka() {
            Polje p1 = new Polje(1, 2);
            Polje p2 = new Polje(1, 2);
            Assert.AreEqual(p1, p2);
        }

        [TestMethod]
        public void Polje_PoljaSaRazlicitimStupcemIRetkomSuRazlicita() {
            Polje p1 = new Polje(1, 2);
            Polje p2 = new Polje(1, 3);
            Assert.AreNotEqual(p1, p2);
        }
    }
}
