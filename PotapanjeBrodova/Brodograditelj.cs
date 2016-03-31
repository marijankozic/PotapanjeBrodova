using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Brodograditelj
    {
        Mreza mreza;
        public Mreza Mreza { get { return mreza; } }


        public Brodograditelj() {
        }

        public Brodograditelj(int redaka, int stupaca) {
            // Nepotrebni konstruktor samo za potrebe Unit Testinga
            // (treba nam nacin da stvorimo Brodograditelja s poznatom velicinom mreze)
            this.mreza = new Mreza(redaka, stupaca);
        }

        public Flota SloziFlotu(int redaka, int stupaca, int[] duljineBrodova) {

            Flota flota = new Flota();
            this.mreza = new Mreza(redaka, stupaca);

            // Gradimo brodove pocevsi od najduljeg -> da se ne upucamo u nogu i
            // zauzmemo previse slobodnih polja s malim brodovima pa veliki ne stanu nigdje
            Array.Sort(duljineBrodova);
            duljineBrodova.Reverse();
            foreach (int duljina in duljineBrodova) {
                Brod b = SagradiBrod(duljina);
                flota.DodajBrod(b);
                PostaviBrodNaMrezu(b);
            }

            return flota;
        }


        // Ovo je algoritam koji smo odabrali na predavanjima.
        // Moj problem s tim algoritmom je sto kao rezultat dobijem pocetno polje. Tocka.
        // Da bi postavio brod, moram znati i smjer tj. moram znati da li je polje iz horizontalne ili vertikalne skupine.
        // Isto tako, konstruktor klase Brod za sada prima samo pocetno polje. Za izracun ostalih polja fali jos i smjer.

        // Umjesto toga, predlazem metodu SagradiBrod koja ce odabrati pocetno polje i smjer te odmah odabrati i ostala polja
        // koja pripadaju brodu. Pozvati ce konstruktor new Brod(listaPoljaBroda) i vratiti gotovi brod Brodograditelju.
        // Nakon toga se nastavlja dogovorenim redom - mreza eliminira polja, brod se dodaje u flotu, itd.

        /*   public Polje IzaberiPocetnoPolje(IEnumerable<Polje> slobodnaPolja, int duljina) {
                 IEnumerable<Polje> horizontalnaPolja = DajHorizontalnaSlobodnaPolja(slobodnaPolja, duljina);
                 IEnumerable<Polje> vertikalnaPolja = DajVertikalnaSlobodnaPolja(slobodnaPolja, duljina);
                 Random rand = new Random();
                 if (rand.Next(1, 2) == 1) {
                     return horizontalnaPolja.ElementAt<Polje>(rand.Next(horizontalnaPolja.Count<Polje>()));
                 }
                 else {
                     return vertikalnaPolja.ElementAt<Polje>(rand.Next(vertikalnaPolja.Count<Polje>()));
                 }
             }

         */

        public Brod SagradiBrod(int duljina) {
            // Brodograditelj ima vlastitu Mrezu --> zna si sam pogledati slobodna polja
            IEnumerable<Polje> slobodnaPolja = this.mreza.DajSlobodnaPolja();
            IEnumerable<Polje> horizontalnaPolja = DajHorizontalnaSlobodnaPolja(slobodnaPolja, duljina);
            IEnumerable<Polje> vertikalnaPolja = DajVertikalnaSlobodnaPolja(slobodnaPolja, duljina);
            Random rand = new Random();
            List<Polje> poljaBroda = new List<Polje>();

            if (rand.Next(1, 2) == 1) {
                Polje pocetno = horizontalnaPolja.ElementAt<Polje>(rand.Next(horizontalnaPolja.Count<Polje>()));
                for (int i = pocetno.Stupac; i < pocetno.Stupac + duljina; i++) {
                    poljaBroda.Add(new Polje(pocetno.Redak, i));
                }
            }
            else {
                Polje pocetno = vertikalnaPolja.ElementAt<Polje>(rand.Next(vertikalnaPolja.Count<Polje>()));
                for (int i = pocetno.Redak; i < pocetno.Redak + duljina; i++) {
                    poljaBroda.Add(new Polje(i, pocetno.Stupac));
                }
            }

            return new Brod(poljaBroda);
        }


        public IEnumerable<Polje> DajVertikalnaSlobodnaPolja(IEnumerable<Polje> slobodnaPolja, int duljina) {
            List<Polje> slobodnaPocetna = new List<Polje>();
            foreach (Polje p in slobodnaPolja) {
                if (ImaDovoljnoMjestaDolje(slobodnaPolja, p, duljina)) {
                    slobodnaPocetna.Add(p);
                }
            }
            return slobodnaPocetna;
        }

        public IEnumerable<Polje> DajHorizontalnaSlobodnaPolja(IEnumerable<Polje> slobodnaPolja, int duljina) {
            List<Polje> slobodnaPocetna = new List<Polje>();
            foreach (Polje p in slobodnaPolja) {
                if (ImaDovoljnoMjestaDesno(slobodnaPolja, p, duljina)) {
                    slobodnaPocetna.Add(p);
                }
            }
            return slobodnaPocetna;
        }

        private bool ImaDovoljnoMjestaDolje(IEnumerable<Polje> slobodnaPolja, Polje p, int duljina) {
            Boolean rezultat = true;
            for (int i = p.Redak; i < p.Redak + duljina; i++) {
                if (!slobodnaPolja.Contains<Polje>(new Polje(i, p.Stupac))) {
                    rezultat = false;
                }
            }
            return rezultat;
        }

        private bool ImaDovoljnoMjestaDesno(IEnumerable<Polje> slobodnaPolja, Polje p, int duljina) {
            Boolean rezultat = true;
            for (int i = p.Stupac; i < p.Stupac + duljina; i++) {
                if (!slobodnaPolja.Contains<Polje>(new Polje(p.Redak, i))) {
                    rezultat = false;
                }
            }
            return rezultat;
        }

        public void PostaviBrodNaMrezu(Brod b) {
            // ova metoda je javna samo radi Unit testinga. Sve osim SloziFlotu bi trebalo biti privatno
            // ostale klase nemaju razloga izravno se mijesati u posao brodograditelja

            /* 
            OVO DOLE NIJE ISTINA. NASA PRAVILA KAZU DA SE SMIJU DIRATI :)
            
                // Pravilo kaze da se brodovi ne smiju dirati.
                // To znaci da iz mreze moramo ukloniti sva polja broda + sva okolna polja.
                // Izabiremo jednostavnost ispred brzine --> ista polja uklanjamo vise puta
                // Dodatna pogodnost je sto se Mreza ne buni ako uklanjamo nepostojece polje --> slobodno uklanjamo preko granica mreze
                foreach (Polje p in b.Polja) {
                    this.mreza.EliminirajPolje(p);
                    this.mreza.EliminirajPolje(new Polje(p.Redak,p.Stupac + 1));
                    this.mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac - 1));
                    this.mreza.EliminirajPolje(new Polje(p.Redak + 1, p.Stupac));
                    this.mreza.EliminirajPolje(new Polje(p.Redak - 1, p.Stupac));
                }*/
            foreach (Polje p in b.Polja) {
                this.mreza.EliminirajPolje(p);
            }
        }



    }
}
