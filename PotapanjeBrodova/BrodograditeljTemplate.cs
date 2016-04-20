using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public abstract class BrodograditeljTemplate : IBrodograditelj
    {
        Mreza mreza;
        public Mreza Mreza { get; set; }
        Random rand = new Random();

        public abstract void PostaviBrodNaMrezu(Brod b);

        virtual public Flota SloziFlotu(int redaka, int stupaca, int[] duljineBrodova) {
            // sve cemo pokusati par puta za slucaj da naletimo na gresku ako brodovi ne stanu na mrezu
            int count = 5;
            while (count > 0) {
                try {
                    Flota flota = new Flota();
                    this.Mreza = new Mreza(redaka, stupaca);

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
                catch (Exception) {
                    count--;
                }
            }
            return null;
        }

        virtual public Brod SagradiBrod(int duljina) {
            // Brodograditelj ima vlastitu Mrezu --> zna si sam pogledati slobodna polja

            IEnumerable<Polje> horizontalnaPolja = this.Mreza.DajHorizontalnaSlobodnaPolja(duljina);
            IEnumerable<Polje> vertikalnaPolja = this.Mreza.DajVertikalnaSlobodnaPolja(duljina);
            
            List<Polje> poljaBroda = new List<Polje>();

            int brojPolja = horizontalnaPolja.Count() + vertikalnaPolja.Count();
            int izbor = rand.Next(brojPolja);

            if (izbor >= horizontalnaPolja.Count()) {
                Polje pocetno = vertikalnaPolja.ElementAt(izbor - horizontalnaPolja.Count());
                for (int i = pocetno.Redak; i < pocetno.Redak + duljina; i++) {
                    poljaBroda.Add(new Polje(i, pocetno.Stupac));
                }
            }
            else {
                Polje pocetno = horizontalnaPolja.ElementAt(izbor);
                for (int i = pocetno.Stupac; i < pocetno.Stupac + duljina; i++) {
                    poljaBroda.Add(new Polje(pocetno.Redak, i));
                }
            }

            return new Brod(poljaBroda);
        }


    }
}
