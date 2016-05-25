using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TestAI
    {

        [TestMethod]
        public void AI_IzvaziMjerenjeBrzine() {
            // zanima nas samo brzina izvrsavanja testa
            AIFactory.Pravila = PravilaIgre.DodirivanjeZabranjeno;
            int[] duljine = new int[] { 4, 3, 2, 1 };
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, duljine);
            ai.Izvazi();
        }

        [TestMethod]
        public void AIFactoryIspravnoDajeAIZaPravila() {
            AITemplate ai = AIFactory.DajAI();
            Assert.IsInstanceOfType(ai, typeof(AIRazmak));
            AIFactory.Pravila = PravilaIgre.DodirivanjeDozvoljeno;
            AITemplate a1 = AIFactory.DajAI();
            Assert.IsInstanceOfType(a1, typeof(AIDodirivanje));
            AIFactory.Pravila = PravilaIgre.DodirivanjeZabranjeno;
            AITemplate a2 = AIFactory.DajAI();
            Assert.IsInstanceOfType(a2, typeof(AIRazmak));
        }

        [TestMethod]
        public void AI_InitializeIspravnoPostavljaParametre() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(5, 5, new int[] { 2, 3, 3, 4 });
            Assert.IsTrue(((List<Polje>)ai.Mreza.DajSlobodnaPolja()).Count == 25);
            Assert.IsTrue(ai.Flota.Count == 4);
            Assert.IsTrue(ai.Flota.Contains(2));
            Assert.IsTrue(ai.Flota.FindAll(x => x == 3).Count == 2);
            Assert.IsTrue(ai.Flota.Contains(4));
        }

        [TestMethod]
        public void AI_NakonPrvogPogotkaTaktikaJeTrazenjeSmjera() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
        }

        [TestMethod]
        public void AI_NakonDvaPogotkaTaktikaJeUnistavanje() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaUnistavanjeRazmak));
        }

        [TestMethod]
        public void AI_NakonPogotkaIPromasajaTaktikaJeTrazenjeSmjera() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
        }

        [TestMethod]
        public void AI_NakonDvaPogotkaIPromasajaTaktikaJeTrazenjeSmjera() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
        }

        [TestMethod]
        public void AI_NakonPotapanjaTaktikaJeNapipavanje() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.potopljen);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaNapipavanjeRazmak));
        }

        [TestMethod]
        public void AI_NakonMukotrpnogPotapanjaTaktikaJeNapipavanje() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.potopljen);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaNapipavanjeRazmak));
        }

        [TestMethod]
        public void AI_MjesovitaSimulacijaIspravnoDajeTaktike() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            Polje p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaNapipavanjeRazmak));
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaNapipavanjeRazmak));
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaUnistavanjeRazmak));
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaTrazenjeSmjeraRazmak));
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaUnistavanjeRazmak));
            ai.ObradiPogodak(rezultatGadjanja.potopljen);
            p = ai.Gadjaj();
            Assert.IsInstanceOfType(ai.Taktika, typeof(TaktikaNapipavanjeRazmak));
        }

        [TestMethod]
        public void AI_ObradiPogodakIspravnoRadiZaPogodak() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2, 3, 3, 4 });
            ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            Assert.IsTrue(ai.zap.trenutnaMeta.Count == 1);
            Assert.IsFalse(ai.Mreza.polja.Contains(ai.zap.gadjanoPolje));
        }

        [TestMethod]
        public void AI_ObradiPogodakIspravnoRadiZaPotapanje() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] {1, 2, 3, 3, 4 });
            ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.potopljen);
            Assert.IsTrue(ai.zap.trenutnaMeta.Count == 0);
            Assert.IsFalse(ai.Mreza.polja.Contains(ai.zap.gadjanoPolje));
            Assert.IsFalse(ai.Flota.Contains(1));
        }

        [TestMethod]
        public void AI_ObradiPogodakIspravnoRadiZaPromasaj() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 1, 2, 3, 3, 4 });
            ai.Gadjaj();
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            Assert.IsTrue(ai.zap.trenutnaMeta.Count == 0);
            Assert.IsFalse(ai.Mreza.polja.Contains(ai.zap.gadjanoPolje));
        }

        [TestMethod]
        public void AI_IzvaziIspravnoRacunaTezinuZaRedak() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(1, 5, new int[] { 4 });
            ai.Izvazi();
            foreach (Polje p in ai.Mreza.polja) {
                if (p.Stupac>0 && p.Stupac<4) {
                    Assert.IsTrue(p.Tezina == 2);
                }
                else {
                    Assert.IsTrue(p.Tezina == 1);
                }
            }
        }

        [TestMethod]
        public void AI_IzvaziIspravnoRacunaTezinuZaStupac() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(5, 1, new int[] { 4 });
            ai.Izvazi();
            foreach (Polje p in ai.Mreza.polja) {
                if (p.Redak > 0 && p.Redak < 4) {
                    Assert.IsTrue(p.Tezina == 2);
                }
                else {
                    Assert.IsTrue(p.Tezina == 1);
                }
            }
        }

        [TestMethod]
        public void AIRazmak_EliminirajBrodIspravnoEliminiraPolja() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            Brod b = new Brod(new List<Polje>() { new Polje(4, 4), new Polje(4, 5), new Polje(4, 6), new Polje(4, 7) });
            List<Polje> prosireniBrod = new List<Polje>();
            ai.EliminirajBrod(b.Polja);
            foreach (Polje p in b.Polja) {
                prosireniBrod.Add(p);
                prosireniBrod.Add(new Polje(p.Redak, p.Stupac + 1));
                prosireniBrod.Add(new Polje(p.Redak, p.Stupac - 1));
                prosireniBrod.Add(new Polje(p.Redak + 1, p.Stupac));
                prosireniBrod.Add(new Polje(p.Redak - 1, p.Stupac));
            }
            Assert.IsFalse(prosireniBrod.Intersect(ai.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void AIRazmak_EliminirajBrodIspravnoEliminiraPoljaZaBrodUUglu() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            Brod b = new Brod(new List<Polje>() { new Polje(0, 0), new Polje(0, 1), new Polje(0, 2), new Polje(0, 3) });
            List<Polje> prosireniBrod = new List<Polje>();
            ai.EliminirajBrod(b.Polja);
            prosireniBrod = b.Polja;
            prosireniBrod.Add(new Polje(1, 0));
            prosireniBrod.Add(new Polje(1, 1));
            prosireniBrod.Add(new Polje(1, 2));
            prosireniBrod.Add(new Polje(1, 3));
            prosireniBrod.Add(new Polje(0, 4));
            Assert.IsFalse(prosireniBrod.Intersect(ai.Mreza.DajSlobodnaPolja()).Any());
        }

        [TestMethod]
        public void AIRazmak_GadjajDajePoljeUnutarMreze() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            ai.Gadjaj();
            Assert.IsTrue(ai.Mreza.polja.Contains(ai.zap.gadjanoPolje));
        }

        [TestMethod]
        public void AIRazmak_GadjajNakonPogotkaDajePoljeDoGadjanogPolja() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            ai.Gadjaj();
            Polje prvoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje drugoPolje = ai.zap.gadjanoPolje;
            Assert.IsTrue((prvoPolje.Redak==drugoPolje.Redak && Math.Abs(prvoPolje.Stupac-drugoPolje.Stupac) ==1)
                || (prvoPolje.Stupac == drugoPolje.Stupac && Math.Abs(prvoPolje.Redak - drugoPolje.Redak) == 1));
        }

        [TestMethod]
        public void AIRazmak_GadjajNakonPogotkaIPromasajaJosUvijekDajePoljeDoGadjanogPolja() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            ai.Gadjaj();
            Polje prvoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje drugoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            ai.Gadjaj();
            Polje trecePolje = ai.zap.gadjanoPolje;
            Assert.IsTrue((prvoPolje.Redak == trecePolje.Redak && Math.Abs(prvoPolje.Stupac - trecePolje.Stupac) == 1)
                || (prvoPolje.Stupac == trecePolje.Stupac && Math.Abs(prvoPolje.Redak - trecePolje.Redak) == 1));
            Assert.AreNotEqual(prvoPolje, drugoPolje);
        }

        [TestMethod]
        public void AIRazmak_GadjajNakonPogotkaIDvaPromasajaJosUvijekDajePoljeDoGadjanogPolja() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 2 });
            ai.Gadjaj();
            Polje prvoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje drugoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            ai.Gadjaj();
            Polje trecePolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            ai.Gadjaj();
            Polje cetvrtoPolje = ai.zap.gadjanoPolje;
            Assert.IsTrue((prvoPolje.Redak == cetvrtoPolje.Redak && Math.Abs(prvoPolje.Stupac - cetvrtoPolje.Stupac) == 1)
                || (prvoPolje.Stupac == cetvrtoPolje.Stupac && Math.Abs(prvoPolje.Redak - cetvrtoPolje.Redak) == 1));
        }

        [TestMethod]
        public void AIRazmak_GadjajNakonDvaPogotkaNastavljaSustavnoGadjatiUIstomSmjeru() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            ai.Gadjaj();
            Polje prvoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje drugoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje trecePolje = ai.zap.gadjanoPolje;
            Assert.IsTrue((prvoPolje.Redak == trecePolje.Redak && Math.Abs(prvoPolje.Stupac - trecePolje.Stupac) == 2)
                || (prvoPolje.Stupac == trecePolje.Stupac && Math.Abs(prvoPolje.Redak - trecePolje.Redak) == 2));
        }

        [TestMethod]
        public void AIRazmak_GadjajNakonDvaPogotkaPaPromasajaNastavljaSustavnoGadjatiUSuprotnomSmjeru() {
            AITemplate ai = AIFactory.DajAI();
            ai.Initialize(10, 10, new int[] { 4 });
            ai.Gadjaj();
            Polje prvoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje drugoPolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.pogodak);
            ai.Gadjaj();
            Polje trecePolje = ai.zap.gadjanoPolje;
            ai.ObradiPogodak(rezultatGadjanja.promasaj);
            ai.Gadjaj();
            Polje cetvrtoPolje = ai.zap.gadjanoPolje;
            Assert.IsTrue((prvoPolje.Redak == cetvrtoPolje.Redak && Math.Abs(prvoPolje.Stupac - cetvrtoPolje.Stupac) == 1)
                || (prvoPolje.Stupac == cetvrtoPolje.Stupac && Math.Abs(prvoPolje.Redak - cetvrtoPolje.Redak) == 1));
        }
    }
}
