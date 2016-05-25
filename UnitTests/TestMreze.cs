using System;
using PotapanjeBrodova;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TestMreze
    {
        [TestMethod]
        public void Mreza_DajSlobodnaPoljaInicijalnoDajeSvaPoljaUMrezi() {
            Mreza m = new Mreza(10, 10);
            Assert.AreEqual(100, m.DajSlobodnaPolja().Count());
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonEliminiranjaJednogPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(new Polje(1,1));
            Assert.AreEqual(99, m.DajSlobodnaPolja().Count());
            Assert.IsFalse(m.DajSlobodnaPolja().Contains(new Polje(1, 1)));
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonEliminiranjaDvaPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(new Polje(1, 1));
            m.EliminirajPolje(new Polje(2, 2));
            Assert.AreEqual(98, m.DajSlobodnaPolja().Count());
            Assert.IsFalse(m.DajSlobodnaPolja().Contains(new Polje(1, 1)));
            Assert.IsFalse(m.DajSlobodnaPolja().Contains(new Polje(2, 2)));
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonDuplogEliminiranjaIstogPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(new Polje(1, 1));
            m.EliminirajPolje(new Polje(1, 1));
            Assert.AreEqual(99, m.DajSlobodnaPolja().Count());
            Assert.IsFalse(m.DajSlobodnaPolja().Contains(new Polje(1, 1)));
        }

        [TestMethod]
        public void Mreza_DajSlobodnaPoljaNakonEliminiranjaNepostojecegPoljaIspravnoVracaOstatak() {
            Mreza m = new Mreza(10, 10);
            m.EliminirajPolje(new Polje(13, 14));
            m.EliminirajPolje(new Polje(14, 15));
            Assert.AreEqual(100, m.DajSlobodnaPolja().Count());
        }

        [TestMethod]
        public void Mreza_ImaDovoljnoMjestaDoljeDobroRadiSNegativniVrijednostima() {
            Mreza m = new Mreza(5, 5);
            Polje p = new Polje(3, 3);
            Polje x = new Polje(0, -1);
            Assert.IsTrue(m.ImaDovoljnoMjestaDolje(p, 2));
            Assert.IsFalse(m.ImaDovoljnoMjestaDolje(p, 3));
            Assert.IsFalse(m.ImaDovoljnoMjestaDolje(x, 1));
        }

        [TestMethod]
        public void Mreza_ImaDovoljnoMjestaDesnoDobroRadiSNegativniVrijednostima() {
            Mreza m = new Mreza(5, 5);
            Polje p = new Polje(3, 3);
            Polje x = new Polje(0, -1);
            Assert.IsTrue(m.ImaDovoljnoMjestaDesno(p, 2));
            Assert.IsFalse(m.ImaDovoljnoMjestaDesno(p, 3));
            Assert.IsFalse(m.ImaDovoljnoMjestaDesno(x, 1));
        }

    }
}
