using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestFlote
    {
        [TestMethod]
        public void Flota_ObradiPogodakIspravnoJavljaPromasaj() {
            List<Polje> polja = new List<Polje> {new Polje(0,1),new Polje(0,2) };
            Brod b = new Brod(polja);
            Flota f = new Flota();
            f.DodajBrod(b);
            rezultatGadjanja rez = f.ObradiPogodak(2, 2);
            Assert.AreEqual(rezultatGadjanja.promasaj, rez);
        }

        [TestMethod]
        public void Flota_ObradiPogodakIspravnoJavljaPogodak() {
            List<Polje> polja = new List<Polje> { new Polje(0, 1), new Polje(0, 2) };
            Brod b = new Brod(polja);
            Flota f = new Flota();
            f.DodajBrod(b);
            rezultatGadjanja rez = f.ObradiPogodak(0, 2);
            Assert.AreEqual(rezultatGadjanja.pogodak, rez);
        }

        [TestMethod]
        public void Flota_ObradiPogodakIspravnoJavljaPotapanje() {
            List<Polje> polja = new List<Polje> { new Polje(0, 1), new Polje(0, 2) };
            Brod b = new Brod(polja);
            Flota f = new Flota();
            f.DodajBrod(b);
            List<Polje> polja2 = new List<Polje> { new Polje(2, 1), new Polje(2, 2) };
            Brod b2 = new Brod(polja2);
            f.DodajBrod(b2);
            rezultatGadjanja rez = f.ObradiPogodak(0, 2);
            rez = f.ObradiPogodak(0, 1);
            Assert.AreEqual(rezultatGadjanja.potopljen, rez);
            Assert.IsFalse(f.Brodovi.Contains(b));
        }

        [TestMethod]
        public void Flota_ObradiPogodakIspravnoJavljaPoraz() {
            List<Polje> polja = new List<Polje> { new Polje(0, 1), new Polje(0, 2) };
            Brod b = new Brod(polja);
            Flota f = new Flota();
            f.DodajBrod(b);
            rezultatGadjanja rez = f.ObradiPogodak(0, 2);
            rez = f.ObradiPogodak(0, 1);
            Assert.AreEqual(rezultatGadjanja.PORAZ, rez);
        }
    }
}
