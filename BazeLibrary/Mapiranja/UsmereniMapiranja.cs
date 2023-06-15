using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skola.Entiteti;
using FluentNHibernate.Mapping;
namespace Skola.Mapiranja
{
    class UsmereniMapiranja:SubclassMap<Usmereni>
    {
        public UsmereniMapiranja()
        {
            Table("Usmereni");
            KeyColumn("IME");

            References(x=>x.NazivSmera).Column("SMER").LazyLoad().Cascade.All();
        }
    }
}
