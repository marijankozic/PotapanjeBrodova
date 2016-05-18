using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaNapipavanjeRazmak : TaktikaTemplate
    {
        public TaktikaNapipavanjeRazmak(AITemplate.Zapovijedi zap, Mreza mreza, List<int> flota) 
            : base(zap, mreza, flota) {
        }

        public override Polje SlijedecePolje() {
            // biramo nasumicno polje izmedju preostalih polja
            // pretpostavljamo da su sva polja vec izvagana
            var najtezi = this.mreza.polja.Max(x => x.Tezina);
            var najtezaGrupa = this.mreza.polja.FindAll(x => x.Tezina == najtezi);
            return najtezaGrupa.ElementAt(zap.rand.Next(najtezaGrupa.Count));
        }
    }
}
