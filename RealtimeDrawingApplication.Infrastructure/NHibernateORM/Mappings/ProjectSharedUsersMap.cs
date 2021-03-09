using FluentNHibernate.Mapping;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.Mappings
{
    public class ProjectSharedUsersMap:ClassMap<ProjectSharedUsersModel>
    {
        public ProjectSharedUsersMap()
        {
            Id(x => x.Id);
            References(x => x.SharedUser);
            References(x => x.Project);
            Map(x => x.IsAllowed);
            Map(x => x.IsEditable);
        }
    }
}
