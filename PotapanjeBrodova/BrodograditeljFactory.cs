using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public enum PravilaIgre { DodirivanjeZabranjeno, DodirivanjeDozvoljeno };

    public abstract class BrodograditeljFactory
    {
        static PravilaIgre pravila = PravilaIgre.DodirivanjeZabranjeno;
        public static PravilaIgre Pravila { get {return pravila;}  set { pravila = value; } }

        public static BrodograditeljTemplate DajBrodograditelja(params int[] x) {
            switch (pravila) {
                case PravilaIgre.DodirivanjeZabranjeno:
                    return new BrodograditeljRazmak();
                case PravilaIgre.DodirivanjeDozvoljeno:
                    return new BrodograditeljDodirivanje();
                default:
                    return new BrodograditeljRazmak();
            }
        }
    }
}
