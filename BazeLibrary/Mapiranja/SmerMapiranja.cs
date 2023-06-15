using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// ///////////////////
/// </summary>
 using FluentNHibernate.Mapping;
using Skola.Mapiranja;


namespace Skola.Mapiranja 
{
    class SmerMapiranja: ClassMap<Skola.Entiteti.Smer>
    {
        public SmerMapiranja()
        {
            Table("Smer");
            Id(x => x.Naziv, "NAZIV").GeneratedBy.Assigned();
            // Map(x => x.imePredmeta, "IMEPREDMETA");
            Map(x => x.maksBrUcenika, "MAX_BR_UCEN");
            //Map(x=>x.imeUcenika,"IME_PREDMETA");
            HasMany(x => x.Ucenici).KeyColumn("NAZIV_SMERA").LazyLoad().Inverse().Cascade.All();
            HasMany(x => x.Usmereni).KeyColumn("SMER").LazyLoad().Inverse().Cascade.All();

            HasManyToMany(x => x.Opsti)
                .Table("PRIPADA")
                .ParentKeyColumn("NAZIV_SMERA")
                .ChildKeyColumn("IME_PREDMETA")
                .Cascade.All()
                .Inverse();  // obavezno



            
        }

    }
}
