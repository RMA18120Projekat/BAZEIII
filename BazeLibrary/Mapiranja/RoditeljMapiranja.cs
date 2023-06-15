using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.Helpers;
//////////////////////////////////////////
using FluentNHibernate.Mapping;
using Skola.Mapiranja;
namespace Skola.Mapiranja
{
    class RoditeljMapiranja : ClassMap<Skola.Entiteti.Roditelj>
    {
        public RoditeljMapiranja()
        {
            Table("RODITELJ");
            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.Ime, "IME");
            Map(x => x.Prezime, "PREZIME");
            Map(x => x.Clan_veca, "CLAN_VECA");

            References(x => x.Ucenik).Column("JMBG_UCENIKA");

            HasMany(x => x.brojeviTelefona).KeyColumn("ID_RODITELJA").LazyLoad().Inverse().Cascade.All();

        }

    }
}
