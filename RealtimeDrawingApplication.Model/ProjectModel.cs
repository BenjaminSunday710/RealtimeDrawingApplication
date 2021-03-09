using RealtimeDrawingApplication.Common;
using System.Collections.Generic;

namespace RealtimeDrawingApplication.Model
{
    public class ProjectModel:IModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual UserModel ProjectCreator { get; set; }
        public virtual IList<DrawingComponentModel> DrawingComponents { get; set; }
        public virtual IList<ProjectSharedUsersModel> ProjectSharedUsers { get; set; }
    }
}