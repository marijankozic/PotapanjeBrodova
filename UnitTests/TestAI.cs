using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestAI
    {

        [TestMethod]
        public void AI_IzvaziMjerenjeBrzine() {
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
    }
}
