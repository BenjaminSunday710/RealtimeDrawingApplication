using RealtimeDrawingApplication.Common;

namespace RealtimeDrawingApplication.Model
{
    public class ProjectSharedUsersModel:IModel
    {
        public virtual int Id { get; set; }
        public virtual UserModel SharedUser  { get; set; }
        public virtual ProjectModel Project { get; set; }
        public virtual bool IsAllowed { get; set; }
        public virtual bool IsEditable { get; set; }
    }
}