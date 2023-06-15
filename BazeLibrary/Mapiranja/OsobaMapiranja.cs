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
    class OsobaMapiranja:ClassMap<Skola.Entiteti.Osoba>
    {
        public OsobaMapiranja()
        {
            Table("Osoba");

            Id(x => x.Jmbg, "JMBG").GeneratedBy.Assigned();

            Map(x => x.Ime, "IME");
            Map(x => x.Prezime, "PREZIME");
            Map(x => x.Adresa, "ADRESA");
        }
       
    }
}
