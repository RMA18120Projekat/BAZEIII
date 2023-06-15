using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
   public  class DobijaOcenu
    {
        public virtual int? ID { get; set; }
        public virtual Ocena Ocena { get; set; }

        public virtual UCenik Ucenik{ get; set; }
        public virtual Predmet Predmet { get; set; }

        public virtual string Datum_upisa_ocene { get; set;  }


        public DobijaOcenu()
        {

        }

    }
}
