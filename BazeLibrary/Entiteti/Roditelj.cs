using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class Roditelj
    {
        public virtual int? Id { get; set; }
        public virtual string Ime { get; set; }
        public virtual string Prezime { get; set; }
        public virtual string Clan_veca { get; set; }
        //public virtual long Jmbg_ucenika { get; set; }
        public virtual UCenik Ucenik { get; set; }

        public virtual IList<BrojTelefona> brojeviTelefona { get; set; }

        public Roditelj()
        {
            brojeviTelefona = new List<BrojTelefona>();
        }
    }
}
