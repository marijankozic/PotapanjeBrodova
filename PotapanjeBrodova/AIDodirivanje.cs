using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class AIDodirivanje : AITemplate
    {
        public AIDodirivanje() {}

        public override void EliminirajBrod(List<Polje> brod) {
            throw new NotImplementedException();
        }

        protected override HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak) {
            throw new NotImplementedException();
        }
    }
}
