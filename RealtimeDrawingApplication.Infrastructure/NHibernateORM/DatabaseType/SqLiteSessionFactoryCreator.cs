using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using RealtimeDrawingApplication.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.NHibernateORM.DatabaseType
{
    class SqLiteSessionFactoryCreator:IDatabaseType
    {
        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("RealtimeDrawingApplicationProject.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if (!File.Exists("RealtimeDrawingApplicationProject.db"))
                new SchemaExport(config)
                  .Create(false, true);
        }
    }
}
