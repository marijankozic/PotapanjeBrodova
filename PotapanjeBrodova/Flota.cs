using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Flota
    {
        List<Brod> brodovi = new List<Brod>();
        public List<Brod> Brodovi { get { return brodovi; } }

        public Flota() {

        }

        public void DodajBrod(Brod brod) {
            this.brodovi.Add(brod);
        }

        public void EliminirajBrod(Brod brod) {
            this.brodovi.Remove(brod);
        }

    }
}
