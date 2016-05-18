using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaUnistavanjeRazmak : TaktikaTemplate
    {
        
        public TaktikaUnistavanjeRazmak(AITemplate.Zapovijedi zap, Mreza mreza, List<int> flota)
            : base(zap, mreza, flota) {

            //    ako smjer jos nije odredjen, racunamo ga iz pogodaka
            Polje prviPogodak = zap.trenutnaMeta.First();
            Polje zadnjiPogodak = zap.trenutnaMeta.Last();
            if (zap.pronadjeniSmjer == smjer.nepoznato) {
                zap.pronadjeniSmjer = OdrediSmjer(prviPogodak, zadnjiPogodak);
            }
        }

        public override Polje SlijedecePolje() {
            // Stanja koja vode u ovaj objekt su:
            // A) drugi (i svaki slijedeci) pogodak broda

            Polje zadnjiPogodak = zap.trenutnaMeta.Last();
            return PoljeZaSmjer(zap.pronadjeniSmjer, zadnjiPogodak);
        }
    }
}
