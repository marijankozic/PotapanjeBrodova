﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public class Flota
    {
        public void DodajBrod(Brod b) {
            brodovi.Add(b);
        }

        public IEnumerable<Brod> Brodovi
        {
            get { return brodovi; }
        }

        public int BrojBrodova
        {
            get { return brodovi.Count; }
        }

        public RezultatGađanja Gađaj(Polje polje) {
            foreach (Brod b in brodovi) {
                var rezultat = b.Gađaj(polje);
                if (rezultat != RezultatGađanja.Promašaj)
                    return rezultat;
            }
            return RezultatGađanja.Promašaj;
        }

        List<Brod> brodovi = new List<Brod>();
    }
}
