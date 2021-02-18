using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.NHibernateORM
{
    public interface IDatabaseType
    {
        ISessionFactory CreateSessionFactory();
    }
}
