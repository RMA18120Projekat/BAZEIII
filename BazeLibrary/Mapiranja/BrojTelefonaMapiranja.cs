using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Skola.Entiteti;
namespace Skola.Mapiranja
{
   public  class BrojTelefonaMapiranja:ClassMap<BrojTelefona>
    {

        public BrojTelefonaMapiranja()
        {
            Table("BrojTelefona");

            Id(x => x.vrednost, "BROJ_TELEFONA").GeneratedBy.Assigned() ; // primary key 

            References(x => x.Roditelj).Column("ID_RODITELJA");
        }
    }
}
