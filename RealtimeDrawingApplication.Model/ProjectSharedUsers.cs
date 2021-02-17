namespace RealtimeDrawingApplication.Model
{
    public class ProjectSharedUsers
    {
        public virtual int Id { get; set; }
        public virtual Project Project { get; set; }
        public virtual User ProjectCreator { get; set; }
        public virtual bool IsAllowed { get; set; }
        public virtual bool IsEditable { get; set; }
    }
}