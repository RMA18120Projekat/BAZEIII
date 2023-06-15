using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using Skola.Entiteti;
namespace Skola.Mapiranja
{
    public class AngazovanMapiranja:ClassMap<Angazovan>
    {
        public AngazovanMapiranja()
        {
            Table("ANGAZOVAN");
            References(x => x.Predmet).Column("NAZIV_PREDMETA");
            References(x => x.Zaposlen).Column("JMBG_NASTAVNIKA");
            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.DatumOd).Column("DATUM_OD");
            Map(x => x.DatumDo).Column("DATUM_DO");
        }

    }
}
