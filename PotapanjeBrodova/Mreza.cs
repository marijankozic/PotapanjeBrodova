using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Mreza
    {
        public List<Polje> polja = new List<Polje>();
        public int BrojRedaka { get;}
        public int BrojStupaca { get;}

        public Mreza(int redak, int stupac) {
            BrojRedaka = redak;
            BrojStupaca = stupac;
            for (int r = 0; r < redak; r++)
            {
                for (int s = 0; s < stupac; s++)
                {
                    polja.Add(new Polje(r, s));
                }
            }
        }


        public IEnumerable<Polje> DajSlobodnaPolja() {
            return polja;
        }


        public void EliminirajPolje(Polje p) {
            this.polja.Remove(p);
        }



    }
}
