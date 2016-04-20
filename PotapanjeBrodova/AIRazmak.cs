using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class AIRazmak : AITemplate
    {
        public AIRazmak() {}

        public override void EliminirajBrod(List<Polje> brod) {
            foreach (Polje p in brod) {
                this.Mreza.EliminirajPolje(p);
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac + 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac - 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak + 1, p.Stupac));
                this.Mreza.EliminirajPolje(new Polje(p.Redak - 1, p.Stupac));
            }
        }

        protected override HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak) {
            HashSet<smjer> rezultat = new HashSet<smjer>();
            //pogledaj da li postoje polja lijevo, desno, gore, dole
            // uzmi u ozbir preostale velicine brodova!

            // gledaj lijevo: pomakni se do kraja segmenta, ako brod stane, dodaj smjer.
            int r = prviPogodak.Redak;
            int s = prviPogodak.Stupac;
            while (this.Mreza.DajSlobodnaPolja().Contains(new Polje(r, s - 1))) {
                s--;
            }
            foreach (int duljina in this.Flota) {
                if (s != prviPogodak.Stupac && this.Mreza.ImaDovoljnoMjestaDesno(new Polje(r, s), duljina)) {
                    rezultat.Add(smjer.lijevo);
                    break;
                }
            }

            // gledaj desno: pomakni se do kraja segmenta, pokusaj se pomaknuti lijevo, dodaj smjer
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.Mreza.DajSlobodnaPolja().Contains(new Polje(r, s + 1))) {
                s++;
            }
            foreach (int duljina in this.Flota) {
                if (s != prviPogodak.Stupac && this.Mreza.ImaDovoljnoMjestaDesno(new Polje(r, s - duljina), duljina)) {
                    rezultat.Add(smjer.desno);
                    break;
                }
            }

            // gledaj gore
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.Mreza.DajSlobodnaPolja().Contains(new Polje(r - 1, s))) {
                r--;
            }
            foreach (int duljina in this.Flota) {
                if (r != prviPogodak.Redak && this.Mreza.ImaDovoljnoMjestaDolje(new Polje(r, s), duljina)) {
                    rezultat.Add(smjer.gore);
                    break;
                }
            }

            // gledaj dolje
            r = prviPogodak.Redak;
            s = prviPogodak.Stupac;
            while (this.Mreza.DajSlobodnaPolja().Contains(new Polje(r + 1, s))) {
                r++;
            }
            foreach (int duljina in this.Flota) {
                if (r != prviPogodak.Redak && this.Mreza.ImaDovoljnoMjestaDolje(new Polje(r - duljina, s), duljina)) {
                    rezultat.Add(smjer.dolje);
                    break;
                }
            }

            return rezultat;
        }
    }
}
