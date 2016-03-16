using System;
using PotapanjeBrodova;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestMreze
    {
        [TestMethod]
        public void Mreza_DajSlobodnaPoljaInicijalnoDajeSvaPoljaUMrezi() {
            Mreza m = new Mreza(10, 10);
            Assert.AreEqual(100, m.DajSlobodnaPolja().Count);
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonEliminiranjaJednogPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(1, 1);
            Assert.AreEqual(99, m.DajSlobodnaPolja().Count);
            Assert.IsFalse(m.DajSlobodnaPolja().Exists(polje => polje.Redak == 1 && polje.Stupac == 1));
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonEliminiranjaDvaPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(1, 1);
            m.EliminirajPolje(2, 2);
            Assert.AreEqual(98, m.DajSlobodnaPolja().Count);
            Assert.IsFalse(m.DajSlobodnaPolja().Exists(polje => polje.Redak == 1 && polje.Stupac == 1));
            Assert.IsFalse(m.DajSlobodnaPolja().Exists(polje => polje.Redak == 2 && polje.Stupac == 2));

        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonDuplogEliminiranjaIstogPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(1, 1);
            m.EliminirajPolje(1, 1);
            Assert.AreEqual(99, m.DajSlobodnaPolja().Count);
            Assert.IsFalse(m.DajSlobodnaPolja().Exists(polje => polje.Redak == 1 && polje.Stupac == 1));

        }

    }
}
