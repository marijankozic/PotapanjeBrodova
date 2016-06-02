using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public interface IPucač
    {
        Polje UputiPucanj();
        void EvidentirajRezultat(RezultatGađanja rezultat);
        IEnumerable<Polje> PogođenaPolja { get; }
    }
}
