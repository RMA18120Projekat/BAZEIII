using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Skola.Mapiranja;
using Skola.Entiteti;
namespace Skola.Mapiranja
{
    class UcenikMapiranja : SubclassMap<UCenik>
    {
        public UcenikMapiranja()
        {
            Table("Ucenik");

            KeyColumn("JMBG");

            Map(x => x.Jub).Column("JUB");
            Map(x => x.Razred).Column("RAZRED");
            Map(x => x.DatumUpisaSmera).Column("DATUM_UPISA_SMERA");

            References(x => x.Naziv_smera).Column("NAZIV_SMERA");

            HasMany(x => x.Roditelji).KeyColumn("JMBG_UCENIKA").LazyLoad().Inverse().Cascade.All();


            HasManyToMany(x => x.Predmeti)
               .Table("SLUSA")
               .ParentKeyColumn("JMBG_UCENIKA")
               .ChildKeyColumn("NAZIV_PREDMETA")
               .Cascade.All()
               .Inverse();


            HasMany(x => x.DobijaOcenuIzPredmeta).KeyColumn("JMBG_UCENIKA").LazyLoad();

        }
    }
}
