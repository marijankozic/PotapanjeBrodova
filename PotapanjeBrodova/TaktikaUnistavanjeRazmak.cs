using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaUnistavanjeRazmak : TaktikaTemplate
    {
        smjer pronadjeni;

        public TaktikaUnistavanjeRazmak(List<Polje> trenutnaMeta, Mreza mreza,
            rezultatGadjanja rezultat, Polje gadjanoPolje, HashSet<smjer> moguciSmjerovi,
            ref smjer pronadjeniSmjer, List<int> flota) : base(trenutnaMeta, mreza, rezultat, gadjanoPolje,
                moguciSmjerovi, flota) {

            //    ako smjer jos nije odredjen, racunamo ga iz pogodaka
            Polje prviPogodak = this.trenutnaMeta.First();
            Polje zadnjiPogodak = this.trenutnaMeta.Last();
            if (pronadjeniSmjer == smjer.nepoznato) {
                pronadjeniSmjer = OdrediSmjer(prviPogodak, zadnjiPogodak);
            }
            pronadjeni = pronadjeniSmjer;
        }

        public override Polje SlijedecePolje() {
            // Stanja koja vode u ovaj objekt su:
            // A) drugi (i svaki slijedeci) pogodak broda

            Polje zadnjiPogodak = this.trenutnaMeta.Last();
            return PoljeZaSmjer(pronadjeni, zadnjiPogodak);
        }
    }
}
