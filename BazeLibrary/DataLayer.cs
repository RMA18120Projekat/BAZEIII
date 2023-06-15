using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
////////////////////////////
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Skola.Mapiranja;
namespace Skola
{
    class DataLayer
    {
        private static ISessionFactory _factory = null;
        private static object objLock = new object();

        public static ISession GetSession()
        {
            if (_factory == null)
            {

                lock (objLock)
                {
                    if (_factory == null)
                    {
                        _factory = CreateSessionFactory();
                    }
                }
            }
            return _factory.OpenSession();
        }
        private static ISessionFactory CreateSessionFactory()
        {

            try

            {

                var cfg = OracleManagedDataClientConfiguration.Oracle10

                .ConnectionString(c =>

                  c.Is("DATA SOURCE=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;PERSIST SECURITY INFO=True;USER ID=S18120;Password=root"));




                return Fluently.Configure()

                  .Database(cfg.ShowSql())

                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OsobaMapiranja>())

                            //.ExposeConfiguration(BuildSchema)

                            .BuildSessionFactory();

            }

            catch (Exception ec)

                {

                
                return null;

                    }




            }
        }
    }
