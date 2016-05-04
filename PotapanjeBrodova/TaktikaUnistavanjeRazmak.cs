using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    class TaktikaUnistavanjeRazmak : TaktikaTemplate
    {
        public TaktikaUnistavanjeRazmak(List<Polje> trenutnaMeta, Mreza mreza,
            rezultatGadjanja rezultat, Polje gadjanoPolje, HashSet<smjer> moguciSmjerovi,
            smjer pronadjeniSmjer, List<int> flota) : base(trenutnaMeta, mreza, rezultat, gadjanoPolje,
                moguciSmjerovi, pronadjeniSmjer, flota) {
        }

        public override Polje SlijedecePolje() {
            throw new NotImplementedException();
        }
    }
}
