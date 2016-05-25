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

        public IEnumerable<Polje> DajVertikalnaSlobodnaPolja(int duljina) {
            List<Polje> slobodnaPocetna = new List<Polje>();
            foreach (Polje p in polja) {
                if (ImaDovoljnoMjestaDolje(p, duljina)) {
                    slobodnaPocetna.Add(p);
                }
            }
            return slobodnaPocetna;
        }

        public IEnumerable<Polje> DajHorizontalnaSlobodnaPolja(int duljina) {
            List<Polje> slobodnaPocetna = new List<Polje>();
            foreach (Polje p in polja) {
                if (ImaDovoljnoMjestaDesno(p, duljina)) {
                    slobodnaPocetna.Add(p);
                }
            }
            return slobodnaPocetna;
        }

        public bool ImaDovoljnoMjestaDolje( Polje p, int duljina) {
            Boolean rezultat = true;
            for (int i = p.Redak; i < p.Redak + duljina; i++) {
                if (!polja.Contains<Polje>(new Polje(i, p.Stupac))) {
                    rezultat = false;
                }
            }
            return rezultat;
        }

        public bool ImaDovoljnoMjestaDesno(Polje p, int duljina) {
            Boolean rezultat = true;
            for (int i = p.Stupac; i < p.Stupac + duljina; i++) {
                if (!polja.Contains<Polje>(new Polje(p.Redak, i))) {
                    rezultat = false;
                }
            }
            return rezultat;
        }

    }
}
