using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotapanjeBrodova
{
    public enum smjer { gore, dolje, lijevo, desno, nepoznato };

    public abstract class AITemplate : IAI
    {
        Mreza mreza;
        public Mreza Mreza
        {get { return mreza; } set {mreza = value; }}

        public List<int> Flota {get{return flota;} set{flota = value;}}
        
        List<int> flota = new List<int>();
        

        // preko ovih varijabli pratimo u kojem rezimu rada se trenutno nalazimo
        // i sto cemo slijedece gadjati
        public TaktikaTemplate Taktika
        {get { return taktika; }}

        public Zapovijedi zap { get; set; }
        public class Zapovijedi
        {
            public Zapovijedi() {}
            public List<Polje> trenutnaMeta = new List<Polje>();
            public Polje gadjanoPolje;
            public smjer pronadjeniSmjer = smjer.nepoznato;
            public HashSet<smjer> moguciSmjerovi = new HashSet<smjer>();
            public rezultatGadjanja rezultatGadjanja;
            public Random rand = new Random();
        }
        
        TaktikaTemplate taktika;
        TaktikaFactory tvornica;

        public void Initialize(int redaka, int stupaca, int[] duljineBrodova) {
            this.Mreza = new Mreza(redaka, stupaca);
            this.Flota = duljineBrodova.ToList();
            this.Flota.Sort();
            this.Flota.Reverse();
            this.tvornica = new TaktikaFactory(this.zap, this.mreza, this.flota);
            this.zap = new Zapovijedi();
            this.zap.rezultatGadjanja = rezultatGadjanja.nepoznato;
        }

        public Polje Gadjaj() {
            Izvazi();
            taktika = tvornica.DajTaktiku();
            this.zap.gadjanoPolje = taktika.SlijedecePolje();
            //this.rezultatGadjanja = rezultatGadjanja.nepoznato;
            return this.zap.gadjanoPolje;
        }

        public void ObradiPogodak(rezultatGadjanja rezultat) {
            this.zap.rezultatGadjanja = rezultat;
            switch (rezultat) {
                case rezultatGadjanja.pogodak:
                    this.zap.trenutnaMeta.Add(this.zap.gadjanoPolje);
                    break;
                case rezultatGadjanja.potopljen:
                    this.zap.trenutnaMeta.Add(this.zap.gadjanoPolje);
                    this.flota.Remove(this.zap.trenutnaMeta.Count);
                    this.zap.trenutnaMeta.Clear();
                    break;
                default:
                    break;
            }
            this.Mreza.EliminirajPolje(this.zap.gadjanoPolje);
        }

        public abstract void EliminirajBrod(List<Polje> brod);

        public void Izvazi() {
            // Za svako polje u mrezi mjerimo tezinu --> broj nacina na
            // koji se neki brod moze staviti na polje

            // prvo resetiraj sve tezine
            foreach (Polje p in this.Mreza.polja) {
                p.Tezina = 0;
            }

            foreach (Polje p in this.Mreza.polja) {
                IzvaziPolje(p);
            }
        }

        virtual public void IzvaziPolje(Polje p) {
            // Ovo se cini dovoljno dobro za sve slucajeve
            // Ali ako neki AI ima pametniju ideju, slobodno implementira nesto bolje

            foreach (int duljina in this.Flota) {
                if (this.Mreza.ImaDovoljnoMjestaDesno(p, duljina)) {
                    for (int i = p.Stupac; i < p.Stupac + duljina; i++) {
                        this.Mreza.polja.First(x => x.Redak == p.Redak && x.Stupac == i).Tezina++;
                    }
                }
                if (this.Mreza.ImaDovoljnoMjestaDolje(p, duljina)) {
                    for (int i = p.Redak; i < p.Redak + duljina; i++) {
                        this.Mreza.polja.First(x => x.Redak == i && x.Stupac == p.Stupac).Tezina++;
                    }
                }
            }
        }

        
    }
}
