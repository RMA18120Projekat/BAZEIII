using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skola.Entiteti;


namespace Skola.Entiteti
{
    public  class Usmereni:Predmet
    {
        public virtual Smer NazivSmera { get; set; }
        public Usmereni()
        {

        }


    }
}
