using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMPotapanje
{
    public class Polje
    {
        public Polje(int redak, int stupac) {
            Redak = redak;
            Stupac = stupac;
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            Polje p = (Polje)obj;
            return (p.Redak == Redak) && (p.Stupac == Stupac);
        }

        public override int GetHashCode() {
            return Redak ^ (Stupac >> 16);
        }

        public readonly int Redak;
        public readonly int Stupac;
    }
}
