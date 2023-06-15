using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Skola.Entiteti;
using Skola.Mapiranja;


namespace Skola.Mapiranja

{
    class NenastavnoOsobljeMapiranja:SubclassMap<NenastavnoOsoblje>
    {
        public NenastavnoOsobljeMapiranja()
        {
            Table("NENASTAVNO_OSOBLJE"); //ime kao u bazi 

            Map(x => x.sektorRada).Column("SEKTOR_RADA");
            Map(x => x.strucnaSprema).Column("STRUCNA_SPREMA");

            KeyColumn("JMBG"); //PK osnovne klase 


        }

    }
}
