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
    class ZaposleniMapiranja : SubclassMap<Zaposleni>
    {
        public ZaposleniMapiranja()
        {
            Table("ZAPOSLENI");

            Map(x => x.DatumRodjenja).Column("DATUM_RODJENJA");
            Map(x => x.TipOsoblja).Column("TIP_OSOBLJA");
            Map(x => x.SektorRada).Column("SEKTOR_RADA");
            Map(x => x.StrucnaSprema).Column("STRUCNA_SPREMA");
            Map(x => x.Norma).Column("NORMA");
            Map(x => x.BrojCasova).Column("BROJ_CASOVA");
            Map(x => x.NazivSkole).Column("NAZIV_SKOLE");
            Map(x => x.Angazovan).Column("ANGAZOVAN");
            HasMany(x => x.Angaovani).KeyColumn("JMBG_NASTAVNIKA").LazyLoad().Inverse().Cascade.All();








            KeyColumn("JMBG"); //PK osnovne klase 

        }
    }
}
