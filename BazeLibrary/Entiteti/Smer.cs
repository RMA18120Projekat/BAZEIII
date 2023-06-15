using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skola.Entiteti;

namespace Skola.Entiteti
{
    public  class Smer
    {
      //  public virtual long ImeUcenika{ get; set; } FK
       // public virtual long imePredmeta { get; set; } FK

        public virtual long maksBrUcenika { get; set; }
        public virtual string Naziv { get; set; }  // PK
        public virtual IList<UCenik> Ucenici { get; set; }
        public virtual IList<Usmereni> Usmereni { get; set; }


        public virtual IList<Opsti> Opsti { get; set; }

        public Smer ()
        {
            Ucenici = new List<UCenik>();
            Usmereni=new List<Usmereni>();

            Opsti = new List<Opsti>();

        }
    }
}
