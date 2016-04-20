using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class BrodograditeljRazmak : BrodograditeljTemplate
    {
        /*
         GRADI BRODOVE KOJI SE NE SMIJU DODIRIVATI
             */

        public BrodograditeljRazmak() {}

        public override void PostaviBrodNaMrezu(Brod b) {
            // ova metoda je javna samo radi Unit testinga. Sve osim SloziFlotu bi trebalo biti privatno
            // ostale klase nemaju razloga izravno se mijesati u posao brodograditelja


            // Pravilo kaze da se brodovi ne smiju dirati.
            // To znaci da iz mreze moramo ukloniti sva polja broda + sva okolna polja.
            // Izabiremo jednostavnost ispred brzine --> ista polja uklanjamo vise puta
            // Dodatna pogodnost je sto se Mreza ne buni ako uklanjamo nepostojece polje --> slobodno uklanjamo preko granica mreze
            foreach (Polje p in b.Polja) {
                this.Mreza.EliminirajPolje(p);
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac + 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak, p.Stupac - 1));
                this.Mreza.EliminirajPolje(new Polje(p.Redak + 1, p.Stupac));
                this.Mreza.EliminirajPolje(new Polje(p.Redak - 1, p.Stupac));
            }

        }
    }
  
}
