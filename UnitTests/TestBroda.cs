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

    
#region nepotrebno
        // Dekompozicija testa BrodIspravnoObradjujePogodak kako bi se zadovoljili formalni uvjeti kolegija :(
        [TestMethod]
        public void BrodIspravnoObradjujePogodakZaPoljeKojeJeUBrodu() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.pogodak);
        }

        [TestMethod]
        public void BrodIspravnoObradjujePogodakZaPoljeKojeNijeUBrodu() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 4)), rezultatGadjanja.promasaj);
        }

        [TestMethod]
        public void BrodIspravnoObradjujePogodakZaZadnjePoljeKojeJeUBrodu() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.pogodak);
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 3)), rezultatGadjanja.potopljen);
        }

        [TestMethod]
        public void BrodIspravnoObradjujePogodakZaPoljeKojeJePonovoPogodjenoAProtivnikSiJeSamKrivStoGadjaPoPraznomKomaduMora() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.pogodak);
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.promasaj);
        }

        [TestMethod]
        public void BrodKojiJePotopljenZauvijekOstajePotopljenIGadjanjeNaIstoMjestoJeSamoBacanjeBombiUMore() {
            Brod b = new Brod(new List<Polje>() { new Polje(2, 2), new Polje(2, 3) });
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.pogodak);
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 3)), rezultatGadjanja.potopljen);
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 3)), rezultatGadjanja.promasaj);
            Assert.AreEqual(b.ObradiPogodak(new Polje(2, 2)), rezultatGadjanja.promasaj);
        }

        #endregion

    }
}
