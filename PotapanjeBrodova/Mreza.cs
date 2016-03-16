using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Mreza
    {
        public Polje[,] polja;
        private int brojRedaka;
        private int brojStupaca;

        public Mreza(int redak, int stupac) {
            brojRedaka = redak;
            brojStupaca = stupac;
            this.polja = new Polje[redak, stupac];
            for (int r = 0; r < redak; r++)
            {
                for (int s = 0; s < stupac; s++)
                {
                    this.polja[r,s] = new Polje(r, s);
                }
            }
        }


        public List<Polje> DajSlobodnaPolja() {
            List<Polje> lista = new List<Polje>();
            for (int r = 0; r < brojRedaka; r++)
            {
                for (int s = 0; s < brojStupaca; s++)
                {
                    if (polja[r,s] != null) lista.Add(polja[r,s]);
                }
            }
            //foreach (Polje p in this.polja)
            //{
            //    if (p!=null) lista.Add(p);
            //}
            return lista;
        }


        public void EliminirajPolje(int redak, int stupac) {
            this.polja[redak, stupac] = null;
        }



    }
}
