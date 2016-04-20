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
        public void Brodograditelj_SloziFlotuVracaFlotuKojaSadrziSveBrodove() {
            BrodograditeljTemplate uljanik = BrodograditeljFactory.DajBrodograditelja();
            int[] duljineBrodova = new int[] {3,1,2,4};
            Flota f = uljanik.SloziFlotu(10, 10, duljineBrodova);
            Assert.IsTrue(f.Brodovi.Count == duljineBrodova.Count());
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
    }
}
