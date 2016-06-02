using System;
using System.Collections.Generic;

namespace OOMPotapanje
{
    public class Brodograditelj
    {
        public Brodograditelj() {
            izbornikPolja = new SlučajniOdabirPočetnogPolja();
            eliminatorPolja = new KlasičniEliminatorPolja();
        }

        public Brodograditelj(IOdabirPočetnogPoljaZaBrod odabirPočetnogPolja, IEliminatorPolja eliminator) {
            izbornikPolja = odabirPočetnogPolja;
            eliminatorPolja = eliminator;
        }

        public Flota SložiFlotu(int redaka, int stupaca, int[] duljineBrodova) {
            const int brojPokušaja = 5;
            for (int i = 0; i < brojPokušaja; ++i) {
                try {
                    Mreža mreža = new Mreža(redaka, stupaca);
                    return SložiBrodove(duljineBrodova, mreža);
                }
                catch (ApplicationException) { }
            }
            // ako ne uspije složiti niti nakon 5 pokušaja, baca iznimku
            throw new ApplicationException();
        }

        private Flota SložiBrodove(int[] duljineBrodova, Mreža mreža) {
            Flota flota = new Flota();
            // za svaku duljinu broda:
            for (int i = 0; i < duljineBrodova.Length; ++i) {
                var slobodnaPolja = mreža.DajSlobodnaPolja();
                var pp = izbornikPolja.IzaberiPočetnoPolje(slobodnaPolja, duljineBrodova[i]);
                var pbr = mreža.DajPoljaZaBrod(pp.Smjer, pp.Polje, duljineBrodova[i]);
                Brod b = new Brod(pbr);
                flota.DodajBrod(b);
                EliminirajPoljaOkoBroda(mreža, pbr);
            }
            return flota;
        }

        private void EliminirajPoljaOkoBroda(Mreža mreža, IEnumerable<Polje> brodskaPolja) {
            IEnumerable<Polje> zaEliminirati = eliminatorPolja.PoljaKojaTrebaUklonitiOkoBroda(brodskaPolja, mreža.Redaka, mreža.Stupaca);
            foreach (Polje p in zaEliminirati)
                mreža.EliminirajPolje(p);
        }

        IOdabirPočetnogPoljaZaBrod izbornikPolja;
        IEliminatorPolja eliminatorPolja;
    }
}