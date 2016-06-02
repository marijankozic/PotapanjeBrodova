using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOMPotapanje
{
    public enum mojRezultatGadjanja { promasaj, pogodak, potopljen, PORAZ, nepoznato };
    public class Protivnik
    {
        // OVA KLASA GLUMI PROTIVNIKA TJ. ODRADJUJE SVE POTREBNE RADNJE
        // BEZ DA MORA IZLOZITI SVOJU IMPLEMENTACIJU PREMA VAN

        Brodograditelj brodograditelj;
        Flota flota;
        Topništvo topnistvo;
        int preostaliBrodovi;
        

        public Protivnik(int redaka, int stupaca, int[] duljineBrodova) {
            brodograditelj = new Brodograditelj();
            flota = brodograditelj.SložiFlotu(redaka, stupaca, duljineBrodova);
            topnistvo = new Topništvo(redaka, stupaca, duljineBrodova);
            preostaliBrodovi = duljineBrodova.Length;
        }

        public Tuple<int,int> Gadjaj() {
            Polje p = topnistvo.UputiPucanj();
            return new Tuple<int, int>(p.Redak, p.Stupac);
        }

        public void ObradiPogodak(mojRezultatGadjanja rez) {
            RezultatGađanja rezProtivnik;
            switch (rez) {
                case mojRezultatGadjanja.promasaj:
                    rezProtivnik = RezultatGađanja.Promašaj;
                    break;
                case mojRezultatGadjanja.pogodak:
                    rezProtivnik = RezultatGađanja.Pogodak;
                    break;
                case mojRezultatGadjanja.potopljen:
                    rezProtivnik = RezultatGađanja.Potonuće;
                    break;
                default:
                    throw new Exception("Krivi rezultat gadjanja");
            }
            topnistvo.ObradiGađanje(rezProtivnik);
        }

        public mojRezultatGadjanja JaviRezultat(Tuple<int,int> koordinate) {
            RezultatGađanja rez = flota.Gađaj(new Polje(koordinate.Item1, koordinate.Item2));
            switch (rez) {
                case RezultatGađanja.Promašaj:
                    return mojRezultatGadjanja.promasaj;
                case RezultatGađanja.Pogodak:
                    return mojRezultatGadjanja.pogodak;
                case RezultatGađanja.Potonuće:
                    preostaliBrodovi--;
                    if (preostaliBrodovi == 0) return mojRezultatGadjanja.PORAZ;
                    return mojRezultatGadjanja.potopljen;
                default:
                    throw new Exception("Krivi rezultat gadjanja");
            }
        }
    }
}
