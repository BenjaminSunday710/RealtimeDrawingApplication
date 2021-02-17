using System.Collections.Generic;

namespace RealtimeDrawingApplication.Model
{
    public class Project
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual User ProjectCreator { get; set; }
        public virtual IList<DrawingComponent> DrawingComponents { get; set; }
        public virtual IList<ProjectSharedUsers> ProjectSharedUsers { get; set; }
    }
}