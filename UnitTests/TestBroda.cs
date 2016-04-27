using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestBroda
    {
        [TestMethod]
        public void BrodSeIspravnoGradi() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.IsTrue(b.Polja.Contains(new Polje(2, 2)));
            Assert.IsTrue(b.Polja.Contains(new Polje(2, 3)));
        }

        [TestMethod]
        public void BrodIspravnoObradjujePogodak() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.IsTrue(b.ObradiPogodak(new Polje(2,2))==rezultatGadjanja.pogodak);
            Assert.IsTrue(b.ObradiPogodak(new Polje(2, 4)) == rezultatGadjanja.promasaj);
            Assert.IsTrue(b.ObradiPogodak(new Polje(2, 3)) == rezultatGadjanja.potopljen);
            Assert.IsTrue(b.ObradiPogodak(new Polje(2,1))==rezultatGadjanja.promasaj);
        }
    }
}
