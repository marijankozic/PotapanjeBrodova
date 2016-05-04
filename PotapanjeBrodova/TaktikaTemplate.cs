using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public abstract class TaktikaTemplate : ITaktika
    {
        protected List<Polje> trenutnaMeta;
        protected Mreza mreza;
        protected rezultatGadjanja rezultat;
        protected Polje gadjanoPolje;
        protected HashSet<smjer> moguciSmjerovi;
        protected smjer pronadjeniSmjer;
        protected Random rand = new Random();
        protected List<int> flota;

        public TaktikaTemplate(List<Polje> trenutnaMeta, Mreza mreza, rezultatGadjanja rezultat,
            Polje gadjanoPolje, HashSet<smjer> moguciSmjerovi, smjer pronadjeniSmjer, List<int> flota) {
            this.trenutnaMeta = trenutnaMeta;
            this.mreza = mreza;
            this.rezultat = rezultat;
            this.gadjanoPolje = gadjanoPolje;
            this.moguciSmjerovi = moguciSmjerovi;
            this.pronadjeniSmjer = pronadjeniSmjer;
            this.flota = flota;
        }

        public abstract Polje SlijedecePolje();
    }
}
