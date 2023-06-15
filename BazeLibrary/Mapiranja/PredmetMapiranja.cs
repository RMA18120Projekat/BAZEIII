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
    class PredmetMapiranja : ClassMap<Skola.Entiteti.Predmet>
    {
        public PredmetMapiranja()
        {
            Table("Predmet");

            Id(x => x.Ime, "IME").GeneratedBy.Assigned();  //ime predmeta

            Map(x => x.Godina, "GODINA");

            //ime_nastavnika ne pisemo trenuitno 

            HasManyToMany( x=>x.Ucenici)
                .Table("SLUSA")
                .ParentKeyColumn("NAZIV_PREDMETA")
                .ChildKeyColumn("JMBG_UCENIKA")
                .Cascade.All();
            HasMany(x => x.Nastavnici).KeyColumn("NAZIV_PREDMETA").LazyLoad().Inverse().Cascade.All();

            HasMany(x => x.DobijaUcenikOcenu).KeyColumn("NAZIV_PREDMETA").LazyLoad();




        }

    }
}
