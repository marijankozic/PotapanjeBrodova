namespace PotapanjeBrodova
{
    public interface IAI
    {
        /*
        Sucelje AI brine se za ucinkovito unistavanje protivnicke flote.
        Samo vodi racuna o modelu protivnicke mreze, stanju flote, itd.
        AI je neovisna crna kutija -> izlaze poruke o polju koje se gadja, 
        ulaze povratne informacije. Sve ostalo je nedostupno izvana.
        */

        void Initialize(int redaka, int stupaca, int[] duljineBrodova);
        Polje Gadjaj();
        void ObradiPogodak(Polje p, rezultatGadjanja rezultat);
    }
}