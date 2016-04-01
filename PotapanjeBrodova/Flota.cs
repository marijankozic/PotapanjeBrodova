using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public enum rezultatGadjanja { promasaj, pogodak, potopljen, PORAZ };

    public class Flota
    /*
    Klasa Flota brine se za sve radnje vezane uz nasu flotu,
    prvenstveno za obradu neprijateljskih pogodaka --> neprijatelj gadja flotu.
    Aktivna obrana (potapanje neprijateljskih brodova) zadaca je zasebne AI klase.
    Pasivna obrana (izmicanje protivnickoj vatri) nazalost nije dozvoljena :(
    */
    {
        List<Brod> brodovi = new List<Brod>();
        public List<Brod> Brodovi { get { return brodovi; } }

        public Flota() { }

        public void DodajBrod(Brod brod) {
            this.brodovi.Add(brod);
        }

        public void EliminirajBrod(Brod brod) {
            this.brodovi.Remove(brod);
        }

        public rezultatGadjanja ObradiPogodak(int redak, int stupac) {
            // Ocekujemo numericke koordinate pogotka.
            // Zatim pitamo svaki brod da li je pogodjen/potopljen.
            // Ako je potopljen i zadnji brod, dojavljuje PORAZ :(

            Polje p = new Polje(redak, stupac);
            foreach (Brod b in this.brodovi) {
                rezultatGadjanja rezultat = b.ObradiPogodak(p);
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        continue; // provjeri iduci brod

                    case rezultatGadjanja.pogodak:
                        // nasli smo nesretni brod, napusti petlju
                        return rezultatGadjanja.pogodak;

                    case rezultatGadjanja.potopljen:
                        EliminirajBrod(b);
                        return this.brodovi.Any() ? rezultatGadjanja.potopljen : rezultatGadjanja.PORAZ;
                }
            }
            return rezultatGadjanja.promasaj;
        }

    }
}
