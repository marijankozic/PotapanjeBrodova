using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotapanjeBrodova
{
    public class AI
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

        // preko ovih varijabli pratimo u kojem rezimu rada se trenutno nalazimo
        // i sto cemo slijedece gadjati
        enum rezimRada { napipavanje, unistavanje };
        rezimRada rezim;
        List<Polje> trenutnaMeta = new List<Polje>();
        Polje slijedecePolje;
        enum smjer { gore, dolje, lijevo, desno, nepoznato };
        smjer pronadjeniSmjer = smjer.nepoznato;
        HashSet<smjer> moguciSmjerovi = new HashSet<smjer>();


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
            if (this.slijedecePolje != null) {
                return this.slijedecePolje;
            }
            else {
                // PLACEHOLDER.
                // Ovdje bi isao neki Thread.Sleep ali nema smisla ako smo na istom threadu!
                throw new NotImplementedException();
            }
        }

        public void ObradiPogodak(Polje p, rezultatGadjanja rezultat) {
            // NAPIPAVANJE
            if (this.rezim == rezimRada.napipavanje) {
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        // obicni promasaj
                        this.mreza.EliminirajPolje(p);
                        Izvazi();
                        this.slijedecePolje = SlijedecePoljeNapipavanje();
                        break;
                    case rezultatGadjanja.pogodak:
                        // prvi pogodak
                        this.rezim = rezimRada.unistavanje;
                        this.trenutnaMeta.Add(p);
                        this.mreza.EliminirajPolje(p);
                        this.slijedecePolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.potopljen:
                        // ovdje dolazimo samo ako slucajno napipamo brod velicine 1
                        this.trenutnaMeta.Add(p);
                        this.flota.Remove(this.trenutnaMeta.Count);
                        this.mreza.EliminirajPolje(p);
                        this.slijedecePolje = SlijedecePoljeNapipavanje();
                        break;
                }
            }
            // UNISTAVANJE
            else {
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        this.mreza.EliminirajPolje(p);
                        this.slijedecePolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.pogodak:
                        this.mreza.EliminirajPolje(p);
                        this.trenutnaMeta.Add(p);
                        this.slijedecePolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.potopljen:
                        // ako smo potopili brod, cistimo varijable i vracamo se na napipavanje
                        this.moguciSmjerovi.Clear();
                        this.pronadjeniSmjer = smjer.nepoznato;
                        this.trenutnaMeta.Add(p);
                        this.flota.Remove(this.trenutnaMeta.Count);
                        this.mreza.EliminirajPolje(p);
                        this.trenutnaMeta.Clear();
                        this.rezim = rezimRada.napipavanje;
                        this.slijedecePolje = SlijedecePoljeNapipavanje();
                        break;
                }
            }
        }

        public void Izvazi() {
            // Za svako polje u mrezi mjerimo tezinu --> broj nacina na
            // koji se neki brod moze staviti na polje

            // prvo resetiraj sve tezine
            foreach (Polje p in this.mreza.polja) {
                p.Tezina = 0;
            }

            foreach (Polje p in this.mreza.polja) {
                IzvaziPolje(p);
            }
        }

        public void IzvaziPolje(Polje p) {
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

        private Polje SlijedecePoljeNapipavanje() {
            var najtezi = this.mreza.polja.Max(x => x.Tezina);
            var najtezaGrupa = this.mreza.polja.FindAll(x => x.Tezina == najtezi);
            return najtezaGrupa.ElementAt(rand.Next(najtezaGrupa.Count));
        }

        private Polje SlijedecePoljeUnistavanje(Polje prethodnoPolje, rezultatGadjanja rezultat) {
            Polje zadnjiPogodak = this.trenutnaMeta.Last();
            Polje prviPogodak = this.trenutnaMeta.First();
            if (this.moguciSmjerovi.Count == 0) {
                moguciSmjerovi = IzracunajMoguceSmjerove(prviPogodak);
            }

            // biramo smjer:
            // 1) ako je poznat, nastavljamo po istom
            // 2) ako smo do sada pogodili samo jedno polje, biramo nasumicno neki od preostalih smjerova
            // 3) ako smo pogodili dva polja, iz njih racunamo smjer
            // 4) ako smo imali smjer i upravo promasili, biramo suprotni smjer
            // 5) ako smo promasili a nismo imali smjer, mijenjamo nasumicno smjer

            // Smjer je poznat: slucajevi 1) i 4)
            if (this.pronadjeniSmjer != smjer.nepoznato) {

                // 1)
                if (rezultat == rezultatGadjanja.pogodak) {
                    switch (this.pronadjeniSmjer) {
                        case smjer.gore:
                            return new Polje(zadnjiPogodak.Redak - 1, zadnjiPogodak.Stupac);
                        case smjer.dolje:
                            return new Polje(zadnjiPogodak.Redak + 1, zadnjiPogodak.Stupac);
                        case smjer.lijevo:
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac - 1);
                        case smjer.desno:
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac + 1);
                    }
                }
                else {

                    // 4)
                    switch (this.pronadjeniSmjer) {
                        case smjer.gore:
                            this.pronadjeniSmjer = smjer.dolje;
                            return new Polje(zadnjiPogodak.Redak + 1, zadnjiPogodak.Stupac);
                        case smjer.dolje:
                            this.pronadjeniSmjer = smjer.gore;
                            return new Polje(zadnjiPogodak.Redak - 1, zadnjiPogodak.Stupac);
                        case smjer.lijevo:
                            this.pronadjeniSmjer = smjer.desno;
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac + 1);
                        case smjer.desno:
                            this.pronadjeniSmjer = smjer.lijevo;
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac - 1);
                    }
                }
            }
            else {
                // Smjer je nepoznat: slucajevi 2), 3) i 5)

                // 2) prvi pogodak ili 5) promasaj smjera
                if (this.trenutnaMeta.Count == 1) {
                    smjer noviSmjer = moguciSmjerovi.ElementAt(rand.Next(moguciSmjerovi.Count));
                    moguciSmjerovi.Remove(noviSmjer);
                    switch (noviSmjer) {
                        case smjer.gore:
                            return new Polje(zadnjiPogodak.Redak - 1, zadnjiPogodak.Stupac);
                        case smjer.dolje:
                            return new Polje(zadnjiPogodak.Redak + 1, zadnjiPogodak.Stupac);
                        case smjer.lijevo:
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac - 1);
                        case smjer.desno:
                            return new Polje(zadnjiPogodak.Redak, zadnjiPogodak.Stupac + 1);
                    }
                }
                else {
                    // 3) upravo smo pogodili drugo polje (za 3 i ostala polja vec imamo odredjen smjer)
                    this.pronadjeniSmjer = OdrediSmjer(this.trenutnaMeta.First(), prethodnoPolje);
                    // pozovimo rekurzivno sami sebe da izracunamo novo polje, sada kada znamo smjer
                    return SlijedecePoljeUnistavanje(prethodnoPolje, rezultat);
                }
            }

            // ova glupost je samo zato sto VS misli da imamo rupu u vracanju vrijednosti
            return new Polje(-1, -1);
        }

        private smjer OdrediSmjer(Polje prvo, Polje drugo) {
            if (prvo.Redak == drugo.Redak) {
                if (prvo.Stupac < drugo.Stupac)
                    return smjer.desno;
                else return smjer.lijevo;
            }
            else {
                if (prvo.Redak < drugo.Redak)
                    return smjer.dolje;
                else return smjer.gore;
            }
        }

        private HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak) {
            HashSet<smjer> rezultat = new HashSet<smjer>();
            //pogledaj da li postoje polja lijevo, desno, gore, dole
            // uzmi u ozbir preostale velicine brodova!

            // gledaj lijevo: pomakni se do kraja segmenta, ako brod stane, dodaj smjer.
            int r = prviPogodak.Redak;
            int s = prviPogodak.Stupac;
            while (this.mreza.DajSlobodnaPolja().Contains(new Polje(r, s - 1))) {
                s--;
            }
            foreach (int duljina in this.flota) {
                if (s != prviPogodak.Stupac && this.mreza.ImaDovoljnoMjestaDesno(new Polje(r, s), duljina)) {
                    rezultat.Add(smjer.lijevo);
                    break;
                }
            }

            // gledaj desno: pomakni se do kraja segmenta, pokusaj se pomaknuti lijevo, dodaj smjer
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.mreza.DajSlobodnaPolja().Contains(new Polje(r, s + 1))) {
                s++;
            }
            foreach (int duljina in this.flota) {
                if (s != prviPogodak.Stupac && this.mreza.ImaDovoljnoMjestaDesno(new Polje(r, s - duljina), duljina)) {
                    rezultat.Add(smjer.desno);
                    break;
                }
            }

            // gledaj gore
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.mreza.DajSlobodnaPolja().Contains(new Polje(r - 1, s))) {
                r--;
            }
            foreach (int duljina in this.flota) {
                if (r != prviPogodak.Redak && this.mreza.ImaDovoljnoMjestaDolje(new Polje(r, s), duljina)) {
                    rezultat.Add(smjer.gore);
                    break;
                }
            }

            // gledaj dolje
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.mreza.DajSlobodnaPolja().Contains(new Polje(r + 1, s))) {
                r++;
            }
            foreach (int duljina in this.flota) {
                if (r != prviPogodak.Redak && this.mreza.ImaDovoljnoMjestaDolje(new Polje(r - duljina, s), duljina)) {
                    rezultat.Add(smjer.dolje);
                    break;
                }
            }

            return rezultat;
        }
    }
}
