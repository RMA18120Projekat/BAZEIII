using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Skola.Mapiranja;

namespace Skola.Mapiranja
{
    class OpstiMapiranja: SubclassMap<Skola.Entiteti.Opsti>
    {

        public OpstiMapiranja()
        {
            Table("Opsti");

            KeyColumn("IME"); // jer nasledjuje PK
            Map(x => x.UkBrojSmerova, "MAX_BR_SMERA"); //mapiranje na sql


            HasManyToMany(x => x.Smerovi)
                .Table("PRIPADA")
                .ParentKeyColumn("IME_PREDMETA")
                .ChildKeyColumn("NAZIV_SMERA")
                .Cascade.All();

        }
    }
}
