using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public class KlasičniEliminatorPolja : IEliminatorPolja
    {
        public IEnumerable<Polje> PoljaKojaTrebaUklonitiOkoBroda(IEnumerable<Polje> brodskaPolja, int redaka, int stupaca) {
            List<Polje> polja = new List<Polje>();
            int redak0 = brodskaPolja.First().Redak - 1;
            if (redak0 < 0)
                redak0 = 0;
            int stupac0 = brodskaPolja.First().Stupac - 1;
            if (stupac0 < 0)
                stupac0 = 0;
            int redak1 = brodskaPolja.Last().Redak + 1;
            if (redak1 >= redaka)
                redak1 = redaka - 1;
            int stupac1 = brodskaPolja.Last().Stupac + 1;
            if (stupac1 >= stupaca)
                stupac1 = stupaca - 1;
            for (int r = redak0; r <= redak1; ++r) {
                for (int s = stupac0; s <= stupac1; ++s)
                    polja.Add(new Polje(r, s));
            }
            return polja;
        }
    }
}
