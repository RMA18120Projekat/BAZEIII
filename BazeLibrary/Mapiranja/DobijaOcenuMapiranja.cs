using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FluentNHibernate.Mapping;
using Skola.Mapiranja;


namespace Skola.Mapiranja
{
     public class DobijaOcenuMapiranja:ClassMap<Entiteti.DobijaOcenu>
    {
        public DobijaOcenuMapiranja()
        {
            Table("DOBIJA");

            Id(x => x.ID).GeneratedBy.TriggerIdentity();

            Map(x => x.Datum_upisa_ocene, "DATUM_UPISA_OCENE");

            //strani kljucevi

            References(x => x.Ocena).Column("ID_OCENE").LazyLoad();
            References(x => x.Predmet).Column("NAZIV_PREDMETA").LazyLoad();
            References(x => x.Ucenik).Column("JMBG_UCENIKA").LazyLoad();


        }

    }
}
