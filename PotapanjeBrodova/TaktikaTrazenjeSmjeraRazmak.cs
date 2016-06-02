using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaTrazenjeSmjeraRazmak : TaktikaTemplate
    {
        public TaktikaTrazenjeSmjeraRazmak(AITemplate.Zapovijedi zap, Mreza mreza, List<int> flota)
            : base(zap, mreza, flota) {
        }

        public HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak) {

            // na tezak nacin smo shvatili da provjera slobodnog mjesta radi gresku jer 
            // u redku tj. stupcu nedostaje PRVI POGODAK --> stvara se rupa koja radi gresku
            // zato privremeno dodajemo to polje nazad, a na kraju ga micemo
            this.mreza.polja.Add(prviPogodak);

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

            // ukloni privremeno dodano polje
            this.mreza.polja.Remove(prviPogodak);
            return rezultat;
        }

        public override Polje SlijedecePolje() {
            // Stanja koja vode u ovaj objekt su:
            // A) prvi pogodak broda (smjer jos nije odabran)
            // B) promasaj nakon sto smo ranije pogodili - treba mijenjati smjer

            Polje prviPogodak = zap.trenutnaMeta.First();
            Polje zadnjiPogodak = zap.trenutnaMeta.Last();
            smjer noviSmjer = smjer.nepoznato;

            if (zap.rezultatGadjanja == rezultatGadjanja.pogodak) {
                // A - izaberi nasumice smjer i gadjaj
                if(zap.moguciSmjerovi.Count==0) zap.moguciSmjerovi = IzracunajMoguceSmjerove(prviPogodak);
                noviSmjer = zap.moguciSmjerovi.ElementAt(zap.rand.Next(zap.moguciSmjerovi.Count));
                zap.moguciSmjerovi.Remove(noviSmjer);
                return PoljeZaSmjer(noviSmjer, zadnjiPogodak);
            }
            else {
                // B - ako postoji, izaberi suprotni smjer i gadjaj od prvog! pogotka
                //     brodovi se ne dodiruju -> nije moguce slucajno pogoditi drugi brod i izazvati zabunu smjera
                zap.pronadjeniSmjer = SuprotniSmjer(zap.pronadjeniSmjer);
                if (zap.pronadjeniSmjer == smjer.nepoznato) {
                    noviSmjer = zap.moguciSmjerovi.ElementAt(zap.rand.Next(zap.moguciSmjerovi.Count));
                    zap.moguciSmjerovi.Remove(noviSmjer);
                    return PoljeZaSmjer(noviSmjer, zadnjiPogodak);
                }
                else {
                    return PoljeZaSmjer(zap.pronadjeniSmjer, prviPogodak);
                }
            }

        }
    }
}
