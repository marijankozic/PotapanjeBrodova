using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class AIRazmak : AITemplate
    {
        public AIRazmak() {}

        public override void EliminirajBrod(List<Polje> brod) {
            foreach (Polje p in brod) {
                this.Mreza.EliminirajPolje(p);
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac + 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac - 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak + 1, p.Stupac));
                this.Mreza.EliminirajPolje(new Polje(p.Redak - 1, p.Stupac));
            }
        }

    }
}
