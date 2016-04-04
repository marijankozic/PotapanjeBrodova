using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotapanjeBrodova
{
    class AI
    /*
    Klasa AI brine se za ucinkovito unistavanje protivnicke flote.
    Sama vodi racuna o modelu protivnicke mreze, stanju flote, itd.
    AI je neovisna crna kutija -> izlaze poruke o polju koje se gadja, 
    ulaze povratne informacije. Sve ostalo je nedostupno izvana.
    */
    {
        Mreza mreza;
        List<int> flota = new List<int>();
        Random rand = new Random();

        // preko ove varijable pratimo u kojem rezimu rada se trenutno nalazimo
        enum rezimRada { napipavanje, unistavanje };
        rezimRada rezim;
        List<Polje> trenutnaMeta = new List<Polje>();
        Polje slijedecePolje;



        public AI(int redaka, int stupaca, int[] duljineBrodova) {
            this.mreza = new Mreza(redaka, stupaca);
            this.flota = duljineBrodova.ToList();
            this.flota.Sort();
            this.flota.Reverse();
            this.rezim = rezimRada.napipavanje;
        }

        public Polje Gadjaj() {
            // ako je novo polje spremno (izracunato) onda vrati novo polje
            // inace cekaj (osim ako odustanemo od multithreadinga)
            return new Polje(0, 0);
        }

        public void ObradiPogodak(Polje p, rezultatGadjanja rezultat) {
            if (this.rezim == rezimRada.napipavanje) {
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        this.mreza.EliminirajPolje(p);
                        Izvazi();
                        this.slijedecePolje = SlijedecePoljeNapipavanje();
                        break;
                    case rezultatGadjanja.pogodak:
                        this.rezim = rezimRada.unistavanje;
                        this.trenutnaMeta.Add(p);
                        break;
                    case rezultatGadjanja.potopljen:
                        // ovdje dolazimo samo ako slucajno potopimo brod velicine 1
                        this.trenutnaMeta.Add(p);
                        this.flota.Remove(this.trenutnaMeta.Count);
                        break;
                }
            }
            else {
                this.slijedecePolje = SlijedecePoljeUnistavanje();
            }


        }

       

        private void Izvazi() {
            // Za svako polje u mrezi mjerimo tezinu --> broj nacina na
            // koji se neki brod moze staviti na polje

            // prvo resetiraj sve tezine
            foreach (Polje p in this.mreza.polja) {
                p.Tezina = 0;
            }

            // za svako polje na koje brod stane, povecaj tezinu 
            foreach (Polje p in this.mreza.polja) {
                foreach (int duljina in this.flota) {
                    if (this.mreza.ImaDovoljnoMjestaDesno(p, duljina)) {
                        for (int i = p.Stupac; i < p.Stupac + duljina; i++) {
                            this.mreza.polja.First(x => x.Redak == p.Redak && x.Stupac == i).Tezina++;
                        }
                    }
                    if (this.mreza.ImaDovoljnoMjestaDolje(p, duljina)) {
                        for (int i = p.Redak; i < p.Redak + duljina; i++) {
                            this.mreza.polja.First(x => x.Redak == i && x.Stupac == p.Stupac).Tezina++;
                        }
                    }
                }
            }
        }

        private Polje SlijedecePoljeNapipavanje() {
            var najtezi = this.mreza.polja.Max(x => x.Tezina);
            var najtezaGrupa = this.mreza.polja.FindAll(x => x.Tezina == najtezi);
            return najtezaGrupa.ElementAt(rand.Next(najtezaGrupa.Count));
        }

        private Polje SlijedecePoljeUnistavanje() {
            throw new NotImplementedException();
        }


    }
}
