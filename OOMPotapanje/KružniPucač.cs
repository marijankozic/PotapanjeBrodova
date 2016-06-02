﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public class KružniPucač : IPucač
    {
        public KružniPucač(Polje prvoPogođeno, Mreža mreža) {
            pogođenaPolja.Add(prvoPogođeno);
            this.mreža = mreža;
        }

        #region Implementacija sučelja IPucač

        public Polje UputiPucanj() {
            Debug.Assert(pogođenaPolja.Count == 1);
            int redak = pogođenaPolja[0].Redak;
            int stupac = pogođenaPolja[0].Stupac;

            List<IEnumerable<Polje>> kandidati = new List<IEnumerable<Polje>>();
            foreach (Smjer smjer in Enum.GetValues(typeof(Smjer))) {
                kandidati.Add(mreža.DajPoljaUZadanomSmjeru(redak, stupac, smjer));
            }
            kandidati.Sort((lista1, lista2) => lista2.Count() - lista1.Count());
            var grupe = kandidati.GroupBy(lista => lista.Count());

            var najdulji = grupe.First();
            int indeks = najdulji.Count() == 1 ? 0 : slučajni.Next(najdulji.Count());
            zadnjeGađano = najdulji.ElementAt(indeks).First();
            mreža.EliminirajPolje(zadnjeGađano);
            return zadnjeGađano;
        }

        public void EvidentirajRezultat(RezultatGađanja rezultat) {
            if (rezultat == RezultatGađanja.Promašaj)
                return;
            pogođenaPolja.Add(zadnjeGađano);
            pogođenaPolja.Sort((a, b) => a.Redak - b.Redak + a.Stupac - b.Stupac);
        }

        public IEnumerable<Polje> PogođenaPolja
        {
            get { return pogođenaPolja; }
        }

        #endregion Implementacija sučelja IPucač

        List<Polje> pogođenaPolja = new List<Polje>();
        Polje zadnjeGađano;
        Mreža mreža;
        Random slučajni = new Random();

    }
}
