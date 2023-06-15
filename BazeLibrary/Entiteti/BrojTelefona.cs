using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skola.Entiteti
{
    public class BrojTelefona
    {
        public  virtual  int vrednost { get; set; }  // pk

        public virtual Roditelj Roditelj { get; set;  } // da znamo kome pripada taj broj telefona 



        public BrojTelefona()
        { }
    }
}
