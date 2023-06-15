using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>


namespace Skola.Entiteti
{
   public  class NenastavnoOsoblje: Zaposleni
    {

        public virtual string sektorRada { get; set; }

        public virtual string strucnaSprema { get; set; }

        public NenastavnoOsoblje()
        {

        }


    }
}
