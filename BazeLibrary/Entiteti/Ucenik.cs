using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Skola.Entiteti
{
    public class UCenik : Osoba
    {
        public virtual int Razred { get; set; }

        public virtual int Jub { get; set; }
        public virtual Smer Naziv_smera { get; set; }
        public virtual IList<Roditelj> Roditelji { get; set; }

        public virtual IList<Predmet> Predmeti { get; set; }

        public virtual IList<DobijaOcenu> DobijaOcenuIzPredmeta { get; set; }
        public virtual string DatumUpisaSmera { get; set; }
        public UCenik()
        {
            Roditelji = new List<Roditelj>();

            Predmeti = new List<Predmet>();

            DobijaOcenuIzPredmeta = new List<DobijaOcenu>();

        }


    }
}
