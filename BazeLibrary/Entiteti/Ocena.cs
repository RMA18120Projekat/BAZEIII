using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public  class Ocena
    {
        public virtual int? ID { get; set; }
        public virtual long Vrednost { get; set; }

        public virtual string tekstOpis { get; set; }

        public virtual  IList<DobijaOcenu> DobijaUcenikIzPredmeta { get; set; }

        public Ocena()
        {
            DobijaUcenikIzPredmeta = new List<DobijaOcenu>();
        }

    }
}
