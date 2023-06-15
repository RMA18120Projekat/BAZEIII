using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Zaposleni : Osoba
    {
        public virtual string DatumRodjenja { get; set; }
        public virtual string TipOsoblja { get; set; }
        public virtual string SektorRada { get; set; }
        public virtual string StrucnaSprema { get; set; }
        public virtual string Norma { get; set; }
        public virtual int? BrojCasova { get; set; }
        public virtual string NazivSkole { get; set; }
        public virtual string Angazovan { get; set; }
        public virtual IList<Angazovan> Angaovani { get; set; }
        public Zaposleni()
        {
            Angaovani = new List<Angazovan>();
        }
    }
}
