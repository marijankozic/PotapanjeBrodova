using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        int[] flota;

        // preko ove varijable pratimo u kojem rezimu rada se trenutno nalazimo
        enum rezimRada { napipavanje, unistavanje};
        rezimRada rezim;



        public AI(int redaka, int stupaca, int[] duljineBrodova) {
            this.mreza = new Mreza(redaka, stupaca);
            this.flota = duljineBrodova;
            this.rezim = rezimRada.napipavanje;
        }

        public Polje Gadjaj() {
            return new Polje(0, 0);
        }

        public void ObradiPogodak(Polje p) {

        }



    }
}
