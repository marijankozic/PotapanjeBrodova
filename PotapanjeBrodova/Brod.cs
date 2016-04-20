using System;
using System.Collections.Generic;

namespace PotapanjeBrodova
{
    public class Brod
    {
        List<Polje> polja;
        public List<Polje> Polja { get { return polja; } }

        public Brod(List<Polje> polja) {
            this.polja = polja;
        }

        public rezultatGadjanja ObradiPogodak(Polje p) {
            if (this.polja.Contains(p)) {
                this.polja.Remove(p);
                return this.polja.Count==0 ? rezultatGadjanja.potopljen : rezultatGadjanja.pogodak;
            }
            else {
                return rezultatGadjanja.promasaj; 
            }
        }

        
        

    }
}