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
            BrodograditeljTemplate graditelj = BrodograditeljFactory.DajBrodograditelja();
            graditelj.Mreza = new Mreza(1, 5);
            Assert.IsTrue(graditelj.Mreza.DajHorizontalnaSlobodnaPolja(4).Count() == 2);
        }

        [TestMethod]
        public void Brodograditelj_DajVertikalnaPocetnaPoljaZaBrodDuljine4Vraca2Polja() {
            BrodograditeljTemplate b = BrodograditeljFactory.DajBrodograditelja();
            b.Mreza = new Mreza(5, 1);
            Assert.IsTrue(b.Mreza.DajVertikalnaSlobodnaPolja(4).Count() == 2);
        }

        [TestMethod]
        public void Brodograditelj_SagradiBrodVracaBrodCijaSvaPoljaSuDioListeSlobodnihPolja() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            uljanik.SloziFlotu(5, 5, new int[] { 1 });
            Brod b = uljanik.SagradiBrod(4);
            Assert.IsFalse(b.Polja.Except(uljanik.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void Brodograditelj_PostaviBrodNaMrezuIspravnoEliminiraPolja() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            uljanik.Mreza = new Mreza(5, 5);
            Brod b = uljanik.SagradiBrod(4);
            uljanik.PostaviBrodNaMrezu(b);
            Assert.IsFalse(b.Polja.Intersect(uljanik.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void BrodograditeljRazmak_PostaviBrodNaMrezuIspravnoEliminiraPolja() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            uljanik.Mreza = new Mreza(10, 10);
            Brod b = uljanik.SagradiBrod(4);
            uljanik.PostaviBrodNaMrezu(b);
            List<Polje> prosireniBrod = new List<Polje>();
            foreach (Polje p in b.Polja) {
                prosireniBrod.Add(p);
                prosireniBrod.Add(new Polje(p.Redak, p.Stupac + 1));
                prosireniBrod.Add(new Polje(p.Redak, p.Stupac - 1));
                prosireniBrod.Add(new Polje(p.Redak + 1, p.Stupac));
                prosireniBrod.Add(new Polje(p.Redak - 1, p.Stupac));
            }
            Assert.IsFalse(prosireniBrod.Intersect(uljanik.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void Brodograditelj_SloziFlotuVracaFlotuKojaSadrziSveBrodove() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            int[] duljineBrodova = new int[] {3,1,2,4};
            Flota f = uljanik.SloziFlotu(10, 10, duljineBrodova);
            Assert.IsTrue(f.Brodovi.Count == duljineBrodova.Count());
        }

        [TestMethod]
        public void Brodograditelj_SloziFlotuVracaFlotuKojaSadrziBasOneBrodoveKojeZelimo() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            int[] duljineBrodova = new int[] { 3, 3, 3, 1, 2, 2, 4, 4, 4, 4 };
            Flota f = uljanik.SloziFlotu(10, 10, duljineBrodova);
            Assert.AreEqual(1, f.Brodovi.Count(brod => brod.Polja.Count == 1));
            Assert.AreEqual(2, f.Brodovi.Count(brod => brod.Polja.Count == 2));
            Assert.AreEqual(3, f.Brodovi.Count(brod => brod.Polja.Count == 3));
            Assert.AreEqual(4, f.Brodovi.Count(brod => brod.Polja.Count == 4));
        }

        [TestMethod]
        public void BrodograditeljFactoryIspravnoDajeBrodograditeljaZaPravila() {
            BrodograditeljTemplate b = BrodograditeljFactory.DajBrodograditelja();
            Assert.IsInstanceOfType(b, typeof(BrodograditeljRazmak));
            BrodograditeljFactory.Pravila = PravilaIgre.DodirivanjeDozvoljeno;
            BrodograditeljTemplate b1 = BrodograditeljFactory.DajBrodograditelja();
            Assert.IsInstanceOfType(b1, typeof(BrodograditeljDodirivanje));
            BrodograditeljFactory.Pravila = PravilaIgre.DodirivanjeZabranjeno;
            BrodograditeljTemplate b2 = BrodograditeljFactory.DajBrodograditelja();
            Assert.IsInstanceOfType(b2, typeof(BrodograditeljRazmak));
        }

        [TestMethod]
        public void Brodograditelj_SagradiBrodVracaBrodKojiJeDioMreze() {
            BrodograditeljTemplate b = BrodograditeljFactory.DajBrodograditelja();
            b.Mreza = new Mreza(10, 10);
            Brod brod = b.SagradiBrod(4);
            foreach (Polje p in brod.Polja) {
                Assert.IsTrue(b.Mreza.polja.Contains(p));
            }
        }
    }
}
