﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public class Polje
    {
        public readonly int Redak;
        public readonly int Stupac;
        public int Tezina { get; set; } // potrebno za AI

        public Polje(int redak, int stupac) {
            Redak = redak;
            Stupac = stupac;
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            Polje p = (Polje)obj;
            
            return (p.Redak==this.Redak)&&(p.Stupac==this.Stupac);
        }

        public override int GetHashCode() {
            // Preblagi Boze koliko vremena mi je trebalo da nadjem ovu gresku!!
            // GetHashCode je nuzan za ispravno baratanje kolekcijama kao skupovima!
            return string.Format("{0},{1}",this.Redak,this.Stupac).GetHashCode();
        }
    }
}
