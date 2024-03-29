﻿using FluentNHibernate.Mapping;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.Mappings
{
    public class ProjectMap:ClassMap<ProjectModel>
    {
        public ProjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.ProjectCreator);
            HasMany(x => x.ProjectSharedUsers).Inverse().Cascade.All();
            HasMany(x => x.DrawingComponents).Inverse().Cascade.All();
        }
    }
}
