using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Osoba
    {
        public virtual long Jmbg { get;   set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Adresa { get; set; }

        
        public Osoba()
        {

        }
    }

    //public class Ucenik : Osoba { }
    //public class Zaposleni : Osoba { }
}
