﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    class TaktikaTrazenjeSmjeraRazmak : TaktikaTemplate
    {
        public TaktikaTrazenjeSmjeraRazmak(List<Polje> trenutnaMeta, Mreza mreza, 
            rezultatGadjanja rezultat, Polje gadjanoPolje, HashSet<smjer> moguciSmjerovi,
            smjer pronadjeniSmjer, List<int> flota) : base(trenutnaMeta, mreza, rezultat, gadjanoPolje,
                moguciSmjerovi, pronadjeniSmjer, flota) {
        }

        protected HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak) {

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

        public override Polje SlijedecePolje() {
            // Stanja koja vode u ovaj objekt su:
            // A) prvi pogodak broda (smjer jos nije odabran)
            // B) promasaj nakon sto smo ranije pogodili - treba mijenjati smjer

            Polje prviPogodak = this.trenutnaMeta.First();
            Polje zadnjiPogodak = this.trenutnaMeta.Last();

            // A
            if (this.rezultat == rezultatGadjanja.pogodak) {
                moguciSmjerovi = IzracunajMoguceSmjerove(prviPogodak);
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

            // B   OVO TREBA REVIDIRATI!!! STO AKO SMO PONOVO FULALI. TREBA MIJENJATI JOS NEKI SMJER A NE SAMO SUPROTNU
            else {
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
           

            



            throw new NotImplementedException();
        }
    }
}
