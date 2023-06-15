using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Angazovan
    {
        public virtual int? Id { get; set; }
        public virtual Zaposleni Zaposlen{get;set;}
        public virtual Predmet Predmet { get; set; }
        public virtual string DatumOd { get; set; }
        public virtual string DatumDo { get; set; }
        public Angazovan()
        {

        }
    }
}
