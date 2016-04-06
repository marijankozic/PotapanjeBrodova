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
            int[] duljine = new int[] { 4, 3, 2, 1 };
            AI ai = new AI(10, 10, duljine);
            ai.Izvazi();
        }


    }
}
