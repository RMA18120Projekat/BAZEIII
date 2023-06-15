using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Opsti : Predmet
    {
        public virtual int UkBrojSmerova { get; set; }
        
        public virtual IList<Smer> Smerovi { get; set; }


        public Opsti()
        {
            Smerovi = new List<Smer>();
        }

    }
}
