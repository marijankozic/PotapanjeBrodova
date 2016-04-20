using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PotapanjeBrodova;

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


    }
}
