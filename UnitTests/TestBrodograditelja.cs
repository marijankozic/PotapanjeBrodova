using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TestBrodograditelja
    {
        [TestMethod]
        public void Brodograditelj_DajHorizontalnaPocetnaPoljaZaBrodDuljine4Vraca2Polja() {
            List<Polje> polja = new List<Polje>(){new Polje(0,0),new Polje(0,1),
                   new Polje(0,2), new Polje(0,3), new Polje(0,4)};
            Brodograditelj b = new Brodograditelj(1,5);
            Assert.IsTrue(b.Mreza.DajHorizontalnaSlobodnaPolja(4).Count() == 2);
        }


        [TestMethod]
        public void Brodograditelj_DajVertikalnaPocetnaPoljaZaBrodDuljine4Vraca2Polja() {
            List<Polje> polja = new List<Polje>(){new Polje(0,0),new Polje(1,0),
                   new Polje(2,0), new Polje(3,0), new Polje(4,0)};
            Brodograditelj b = new Brodograditelj(5,1);
            Assert.IsTrue(b.Mreza.DajVertikalnaSlobodnaPolja(4).Count() == 2);
        }


        //
        //  Ovaj test odnosi se na verziju metode s predavanja
        //[TestMethod]
        //public void Brodograditelj_IzaberiPocetnoPoljeVraca1PoljeIzNiza() {
        //    Mreza m = new Mreza(5, 5);
        //    IEnumerable<Polje> svaPolja = m.DajSlobodnaPolja();
        //    Brodograditelj b = new Brodograditelj();
        //    Polje p = b.IzaberiPocetnoPolje(svaPolja, 4);
        //    Assert.IsTrue(svaPolja.Contains<Polje>(p));
        //}

        [TestMethod]
        public void Brodograditelj_SagradiBrodVracaBrodCijaSvaPoljaSuDioListeSlobodnihPolja() {
            Brodograditelj uljanik = new Brodograditelj(5,5);
            Brod b = uljanik.SagradiBrod(4);
            Assert.IsFalse(b.Polja.Except(uljanik.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void Brodograditelj_PostaviBrodNaMrezuIspravnoEliminiraPolja() {
            Brodograditelj uljanik = new Brodograditelj(5,5);
            Brod b = uljanik.SagradiBrod(4);
            uljanik.PostaviBrodNaMrezu(b);
            Assert.IsFalse(b.Polja.Intersect(uljanik.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void Brodograditelj_SloziFlotuVracaFlotuKojaSadrziSveBrodove() {
            Brodograditelj uljanik = new Brodograditelj();
            int[] duljineBrodova = new int[] {3,1,2,4};
            Flota f = uljanik.SloziFlotu(10, 10, duljineBrodova);
            Assert.IsTrue(f.Brodovi.Count == duljineBrodova.Count());
        }


    }
}
