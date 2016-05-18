using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class TaktikaFactory
    {
        /*
         * Ovisno o stanju AI klase, kreira odgovarajucu taktiku.
         * U ovoj verziji kreira samo Taktike za Pravila igre u kojima se brodovi ne smiju dodirivati
         */

        AITemplate.Zapovijedi zap;
        Mreza mreza;
        List<int> flota;
        
        public TaktikaFactory(AITemplate.Zapovijedi zap, Mreza mreza, List<int> flota ) {
            this.zap = zap;
            this.mreza = mreza;
            this.flota = flota;
        }

        public TaktikaTemplate DajTaktiku() {
            if (zap.trenutnaMeta.Count == 0)
                return new TaktikaNapipavanjeRazmak(zap, mreza, flota);
            if (zap.trenutnaMeta.Count == 1 ) {
                // jos nemamo trenutnu metu - ili napipavamo dalje ili tek prelazimo u fazu trazenja smjera
                // osim ako je brod vec potopljen
                switch (zap.rezultatGadjanja) {
                    case rezultatGadjanja.promasaj:
                        return new TaktikaTrazenjeSmjeraRazmak(zap, mreza, flota);
                    case rezultatGadjanja.pogodak:
                        return new TaktikaTrazenjeSmjeraRazmak(zap, mreza, flota);
                    case rezultatGadjanja.potopljen:
                        return new TaktikaNapipavanjeRazmak(zap, mreza, flota);
                    case rezultatGadjanja.PORAZ:
                        return null; // OVO NIJE PROBLEM JER SMO U SLUCAJU POBJEDE IONAKO ZOBISLI TRAZENJE POLJA
                    case rezultatGadjanja.nepoznato:
                        return new TaktikaNapipavanjeRazmak(zap, mreza, flota);
                }
            }
            else {
                // imamo do sada barem jedan pogodak -> ili nastavljamo traziti smjer ili idemo na unistavanje
                // osim ako je brod vec potopljen
                switch (zap.rezultatGadjanja) {
                    case rezultatGadjanja.promasaj:
                        return new TaktikaTrazenjeSmjeraRazmak(zap, mreza, flota);
                    case rezultatGadjanja.pogodak:
                        return new TaktikaUnistavanjeRazmak(zap, mreza, flota);
                    case rezultatGadjanja.potopljen:
                        return new TaktikaNapipavanjeRazmak(zap, mreza, flota);
                    case rezultatGadjanja.PORAZ:
                        return null;
                }
            }
            return null; // OPET SE VISUAL STUDIO PRAVI GLUP
        }
    }
}
