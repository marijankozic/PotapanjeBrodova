using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PotapanjeBrodova
{
    public interface ITaktika
    {
        /*
         * Objekti koji naslijedjuju interface ITaktika mojau samo
         * izabrati slijedece polje za gadjanje.
         * Kriterij za odabir su trenutni rezim rada i dosadasnji rezultati gadjanja.
         */

        Polje SlijedecePolje();
    }
}
