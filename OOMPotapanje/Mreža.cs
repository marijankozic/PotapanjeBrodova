using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public enum Orijentacija
    {
        Horizontalno,
        Vertikalno
    }

    public enum Smjer
    {
        Gore,
        Desno,
        Dolje,
        Lijevo
    }

    public class Mreža
    {
        public Mreža(int redaka, int stupaca) {
            Redaka = redaka;
            Stupaca = stupaca;
            polja = new Polje[Redaka, Stupaca];
            for (int r = 0; r < redaka; ++r) {
                for (int s = 0; s < stupaca; ++s)
                    polja[r, s] = new Polje(r, s);
            }
        }

        public IEnumerable<Polje> DajSlobodnaPolja() {
            // umjesto donje dvije petlje može i kraće (ali puno sporije):
            //return polja.Cast<Polje>().Where(polje => polje != null).ToList();
            List<Polje> slobodnaPolja = new List<Polje>();
            for (int r = 0; r < Redaka; ++r) {
                for (int s = 0; s < Stupaca; ++s) {
                    if (polja[r, s] != null)
                        slobodnaPolja.Add(polja[r, s]);
                }
            }
            return slobodnaPolja;
        }

        public void EliminirajPolje(Polje p) {
            polja[p.Redak, p.Stupac] = null;
        }

        public void EliminirajPolje(int redak, int stupac) {
            polja[redak, stupac] = null;
        }

        public IEnumerable<Polje> DajPoljaZaBrod(Orijentacija smjer, Polje početno, int duljinaBroda) {
            int redak = početno.Redak;
            int stupac = početno.Stupac;
            int deltaRedak = smjer == Orijentacija.Horizontalno ? 0 : 1;
            int deltaStupac = smjer == Orijentacija.Vertikalno ? 0 : 1;
            List<Polje> poljaZaBrod = new List<Polje>();
            for (int i = 0; i < duljinaBroda; ++i) {
                poljaZaBrod.Add(polja[redak, stupac]);
                EliminirajPolje(redak, stupac);
                redak += deltaRedak;
                stupac += deltaStupac;
            }
            return poljaZaBrod;
        }

        public IEnumerable<Polje> DajPoljaUZadanomSmjeru(int redak, int stupac, Smjer smjer) {
            switch (smjer) {
                case Smjer.Gore:
                    return DajPoljaIznad(redak, stupac);
                case Smjer.Desno:
                    return DajPoljaDesno(redak, stupac);
                case Smjer.Dolje:
                    return DajPoljaIspod(redak, stupac);
                case Smjer.Lijevo:
                    return DajPoljaLijevo(redak, stupac);
                default:
                    Debug.Assert(false, string.Format("Nije podržan smjer: {0}", smjer.ToString()));
                    return null;
            }
        }

        private IEnumerable<Polje> DajPoljaIznad(int redak, int stupac) {
            List<Polje> slobodna = new List<Polje>();
            while (--redak >= 0 && polja[redak, stupac] != null)
                slobodna.Add(polja[redak, stupac]);
            return slobodna;
        }

        private IEnumerable<Polje> DajPoljaDesno(int redak, int stupac) {
            List<Polje> slobodna = new List<Polje>();
            while (++stupac < Stupaca && polja[redak, stupac] != null)
                slobodna.Add(polja[redak, stupac]);
            return slobodna;
        }

        private IEnumerable<Polje> DajPoljaIspod(int redak, int stupac) {
            List<Polje> slobodna = new List<Polje>();
            while (++redak < Redaka && polja[redak, stupac] != null)
                slobodna.Add(polja[redak, stupac]);
            return slobodna;
        }

        private IEnumerable<Polje> DajPoljaLijevo(int redak, int stupac) {
            List<Polje> slobodna = new List<Polje>();
            while (--stupac >= 0 && polja[redak, stupac] != null)
                slobodna.Add(polja[redak, stupac]);
            return slobodna;
        }

        private Polje[,] polja;
        public readonly int Redaka;
        public readonly int Stupaca;
    }
}
