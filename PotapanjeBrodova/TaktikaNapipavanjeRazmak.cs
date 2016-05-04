using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaNapipavanjeRazmak : TaktikaTemplate
    {
        public TaktikaNapipavanjeRazmak(List<Polje> trenutnaMeta, Mreza mreza,
            rezultatGadjanja rezultat, Polje gadjanoPolje, HashSet<smjer> moguciSmjerovi,
            smjer pronadjeniSmjer, List<int> flota) : base(trenutnaMeta, mreza, rezultat, gadjanoPolje,
                moguciSmjerovi, pronadjeniSmjer, flota) {
        }

        public override Polje SlijedecePolje() {
            // biramo nasumicno polje izmedju preostalih polja
            // pretpostavljamo da su sva polja vec izvagana
            var najtezi = this.mreza.polja.Max(x => x.Tezina);
            var najtezaGrupa = this.mreza.polja.FindAll(x => x.Tezina == najtezi);
            return najtezaGrupa.ElementAt(rand.Next(najtezaGrupa.Count));
        }
    }
}
