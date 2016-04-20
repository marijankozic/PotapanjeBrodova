using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public abstract class AIFactory
    {
        static PravilaIgre pravila = PravilaIgre.DodirivanjeZabranjeno;
        public static PravilaIgre Pravila { get { return pravila; } set { pravila = value; } }

        public static AITemplate DajAI() {
            switch (pravila) {
                case PravilaIgre.DodirivanjeZabranjeno:
                    return new AIRazmak();
                case PravilaIgre.DodirivanjeDozvoljeno:
                    return new AIDodirivanje();
                default:
                    return new AIRazmak();
            }
        }
    }

}
