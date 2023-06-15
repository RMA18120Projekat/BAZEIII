using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Predmet
    {
        public virtual string Ime { get; set; }
        public virtual int Godina { get; set; }

        public virtual IList<UCenik> Ucenici { get; set; }
        public virtual IList<Angazovan> Nastavnici { get; set; }

        public virtual IList<DobijaOcenu> DobijaUcenikOcenu { get; set; }

       // public virtual string imeNastavnika { get; set; }
        public Predmet()
        {
            Ucenici = new List<UCenik>(); 
            Nastavnici=new List<Angazovan>();

            DobijaUcenikOcenu = new List<DobijaOcenu>();



        }
    }
}
