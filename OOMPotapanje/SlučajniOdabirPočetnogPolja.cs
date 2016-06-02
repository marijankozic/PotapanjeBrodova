using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public class SlučajniOdabirPočetnogPolja : IOdabirPočetnogPoljaZaBrod
    {
        public PoljeSmjer IzaberiPočetnoPolje(IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            var horizontalnaPolja = DajHorizontalnaPočetnaPolja(slobodnaPolja, duljinaBroda);
            var vertikalnaPolja = DajVertikalnaPočetnaPolja(slobodnaPolja, duljinaBroda);
            int ukupnoKandidata = horizontalnaPolja.Count() + vertikalnaPolja.Count();
            if (ukupnoKandidata == 0)
                throw new ApplicationException();
            int izbor = slučajni.Next(0, ukupnoKandidata);
            if (izbor >= horizontalnaPolja.Count())
                return new PoljeSmjer(Orijentacija.Vertikalno, vertikalnaPolja.ElementAt(izbor - horizontalnaPolja.Count()));
            return new PoljeSmjer(Orijentacija.Horizontalno, horizontalnaPolja.ElementAt(izbor));
        }

        public IEnumerable<Polje> DajHorizontalnaPočetnaPolja(IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            return DajPočetnaPolja(ImaDovoljnoPoljaDesno, slobodnaPolja, duljinaBroda);
        }

        public IEnumerable<Polje> DajVertikalnaPočetnaPolja(IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            return DajPočetnaPolja(ImaDovoljnoPoljaIspod, slobodnaPolja, duljinaBroda);
        }

        private IEnumerable<Polje> DajPočetnaPolja(Func<Polje, IEnumerable<Polje>, int, bool> funkcija, IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            List<Polje> polja = new List<Polje>();
            foreach (Polje p in slobodnaPolja) {
                if (funkcija(p, slobodnaPolja, duljinaBroda))
                    polja.Add(p);
            }
            return polja;
        }

        private bool ImaDovoljnoPoljaDesno(Polje p, IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            int redak = p.Redak;
            int stupac = p.Stupac;
            for (int s = stupac + 1; s < stupac + duljinaBroda; ++s)
                if (!slobodnaPolja.Contains(new Polje(redak, s)))
                    return false;
            return true;
        }

        private bool ImaDovoljnoPoljaIspod(Polje p, IEnumerable<Polje> slobodnaPolja, int duljinaBroda) {
            int redak = p.Redak;
            int stupac = p.Stupac;
            for (int r = redak + 1; r < redak + duljinaBroda; ++r)
                if (!slobodnaPolja.Contains(new Polje(r, stupac)))
                    return false;
            return true;
        }

        Random slučajni = new Random();
    }
}
