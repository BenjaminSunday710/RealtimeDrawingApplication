using FluentNHibernate.Mapping;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.FullName);
            Map(x => x.Email);
            Map(x => x.Password);
        }
    }
}
