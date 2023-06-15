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
    class OcenaMapiranja : ClassMap<Skola.Entiteti.Ocena>
    {

        public OcenaMapiranja()
        {
            Table("Ocena");


            Id(x => x.ID, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.Vrednost, "VREDNOST");
            Map(x => x.tekstOpis, "TEKST_OPIS");

            HasMany(x => x.DobijaUcenikIzPredmeta).KeyColumn("ID_OCENE").LazyLoad();
            
        }
    }

}

