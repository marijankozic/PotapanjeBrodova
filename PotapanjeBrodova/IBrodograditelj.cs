namespace PotapanjeBrodova
{
    public interface IBrodograditelj
    {
        void PostaviBrodNaMrezu(Brod b);
        Brod SagradiBrod(int duljina);
        Flota SloziFlotu(int redaka, int stupaca, int[] duljineBrodova);
    }
}