using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotapanjeBrodova
{
    public abstract class AITemplate : IAI
    
    {
        Mreza mreza;
        public Mreza Mreza
        {get { return mreza; } set {mreza = value; }}

        public List<int> Flota {get{return flota;} set{flota = value;}}

     

        List<int> flota = new List<int>();
        Random rand = new Random();

        // preko ovih varijabli pratimo u kojem rezimu rada se trenutno nalazimo
        // i sto cemo slijedece gadjati
        public enum rezimRada { napipavanje, trazenjeSmjera, unistavanje };
        rezimRada rezim;
        public rezimRada Rezim
        { get {return rezim;}}
        List<Polje> trenutnaMeta = new List<Polje>();
        Polje gadjanoPolje;
        public enum smjer { gore, dolje, lijevo, desno, nepoznato };
        smjer pronadjeniSmjer = smjer.nepoznato;
        HashSet<smjer> moguciSmjerovi = new HashSet<smjer>();

        public void Initialize(int redaka, int stupaca, int[] duljineBrodova) {
            this.Mreza = new Mreza(redaka, stupaca);
            this.Flota = duljineBrodova.ToList();
            this.Flota.Sort();
            this.Flota.Reverse();
            this.rezim = rezimRada.napipavanje;
        }

        public Polje Gadjaj() {
            // ako je novo polje spremno (izracunato) onda vrati novo polje
            if (this.gadjanoPolje == null) {
                // ako nema polja, moramo nasumicno izabrati jedno polje
                Izvazi();
                this.gadjanoPolje = SlijedecePoljeNapipavanje();
            }
            return this.gadjanoPolje;
        }

        public void ObradiPogodak(rezultatGadjanja rezultat) {
            Polje p = this.gadjanoPolje;
            // NAPIPAVANJE
            if (this.rezim == rezimRada.napipavanje) {
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        // obicni promasaj
                        this.Mreza.EliminirajPolje(p);
                        Izvazi();
                        this.gadjanoPolje = SlijedecePoljeNapipavanje();
                        break;
                    case rezultatGadjanja.pogodak:
                        // prvi pogodak
                        this.rezim = rezimRada.unistavanje;
                        this.trenutnaMeta.Add(p);
                        this.Mreza.EliminirajPolje(p);
                        this.gadjanoPolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.potopljen:
                        // ovdje dolazimo samo ako slucajno napipamo brod velicine 1
                        this.trenutnaMeta.Add(p);
                        this.Flota.Remove(this.trenutnaMeta.Count);
                        EliminirajBrod(trenutnaMeta);
                        this.trenutnaMeta.Clear();
                        this.gadjanoPolje = SlijedecePoljeNapipavanje();
                        break;
                }
            }
            // UNISTAVANJE
            else {
                switch (rezultat) {
                    case rezultatGadjanja.promasaj:
                        this.Mreza.EliminirajPolje(p);
                        this.gadjanoPolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.pogodak:
                        this.Mreza.EliminirajPolje(p);
                        this.trenutnaMeta.Add(p);
                        this.gadjanoPolje = SlijedecePoljeUnistavanje(p, rezultat);
                        break;
                    case rezultatGadjanja.potopljen:
                        // ako smo potopili brod, cistimo varijable i vracamo se na napipavanje
                        this.moguciSmjerovi.Clear();
                        this.pronadjeniSmjer = smjer.nepoznato;
                        this.trenutnaMeta.Add(p);
                        this.Flota.Remove(this.trenutnaMeta.Count);
                        EliminirajBrod(this.trenutnaMeta);
                        this.trenutnaMeta.Clear();
                        this.rezim = rezimRada.napipavanje;
                        this.gadjanoPolje = SlijedecePoljeNapipavanje();
                        break;
                }
            }
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

        virtual public Polje SlijedecePoljeNapipavanje() {
            var najtezi = this.Mreza.polja.Max(x => x.Tezina);
            var najtezaGrupa = this.Mreza.polja.FindAll(x => x.Tezina == najtezi);
            return najtezaGrupa.ElementAt(rand.Next(najtezaGrupa.Count));
        }

        protected Polje SlijedecePoljeUnistavanje(Polje prethodnoPolje, rezultatGadjanja rezultat) {
            Polje zadnjiPogodak = this.trenutnaMeta.Last();
            Polje prviPogodak = this.trenutnaMeta.First();
            if (this.moguciSmjerovi.Count == 0) {
                moguciSmjerovi = IzracunajMoguceSmjerove(prviPogodak);
            }

            // biramo smjer:
            // 1) ako je poznat, nastavljamo po istom
            // 2) ako smo do sada pogodili samo jedno polje, biramo nasumicno neki od preostalih smjerova
            // 3) ako smo pogodili dva polja, iz njih racunamo smjer
            // 4) ako smo imali smjer i upravo promasili, biramo suprotni smjer
            // 5) ako smo promasili a nismo imali smjer, mijenjamo nasumicno smjer

            // Smjer je poznat: slucajevi 1) i 4)
            if (this.pronadjeniSmjer != smjer.nepoznato) {

                // 1)
                if (rezultat == rezultatGadjanja.pogodak) {
                    switch (this.pronadjeniSmjer) {
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
                else {

                    // 4)
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
            }
            else {
                // Smjer je nepoznat: slucajevi 2), 3) i 5)

                // 2) prvi pogodak ili 5) promasaj smjera
                if (this.trenutnaMeta.Count == 1) {
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
                else {
                    // 3) upravo smo pogodili drugo polje (za 3 i ostala polja vec imamo odredjen smjer)
                    this.pronadjeniSmjer = OdrediSmjer(this.trenutnaMeta.First(), prethodnoPolje);
                    // pozovimo rekurzivno sami sebe da izracunamo novo polje, sada kada znamo smjer
                    return SlijedecePoljeUnistavanje(prethodnoPolje, rezultat);
                }
            }

            // ova glupost je samo zato sto VS misli da imamo rupu u vracanju vrijednosti
            return new Polje(-1, -1);
        }

        protected smjer OdrediSmjer(Polje prvo, Polje drugo) {
            if (prvo.Redak == drugo.Redak) {
                if (prvo.Stupac < drugo.Stupac)
                    return smjer.desno;
                else return smjer.lijevo;
            }
            else {
                if (prvo.Redak < drugo.Redak)
                    return smjer.dolje;
                else return smjer.gore;
            }
        }

        abstract protected HashSet<smjer> IzracunajMoguceSmjerove(Polje prviPogodak);
    }
}
