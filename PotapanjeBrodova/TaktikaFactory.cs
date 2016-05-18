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

        AITemplate ai;
        public TaktikaFactory(AITemplate ai) {
            this.ai = ai;
        }

        public TaktikaTemplate DajTaktiku() {
            if (ai.trenutnaMeta.Count == 0) {
                // jos nemamo trenutnu metu - ili napipavamo dalje ili tek prelazimo u fazu trazenja smjera
                // osim ako je brod vec potopljen
                switch (ai.rezultatGadjanja) {
                    case rezultatGadjanja.promasaj:
                        return new TaktikaNapipavanjeRazmak( ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje,ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.pogodak:
                        return new TaktikaTrazenjeSmjeraRazmak(ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje, ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.potopljen:
                        return new TaktikaNapipavanjeRazmak(ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje, ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.PORAZ:
                        return null; // OVO NIJE PROBLEM JER SMO U SLUCAJU POBJEDE IONAKO ZOBISLI TRAZENJE POLJA
                }
            }
            else {
                // imamo do sada barem jedan pogodak -> ili nastavljamo traziti smjer ili idemo na unistavanje
                // osim ako je brod vec potopljen
                switch (ai.rezultatGadjanja) {
                    case rezultatGadjanja.promasaj:
                        return new TaktikaTrazenjeSmjeraRazmak(ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje, ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.pogodak:
                        return new TaktikaUnistavanjeRazmak(ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje, ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.potopljen:
                        return new TaktikaNapipavanjeRazmak(ai.trenutnaMeta, ai.Mreza, ai.rezultatGadjanja, ai.gadjanoPolje, ai.moguciSmjerovi, ai.pronadjeniSmjer, ai.Flota);
                    case rezultatGadjanja.PORAZ:
                        return null;
                }
            }
            return null; // OPET SE VISUAL STUDIO PRAVI GLUP
        }
    }
}
