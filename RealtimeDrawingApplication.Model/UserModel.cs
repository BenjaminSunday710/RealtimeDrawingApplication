using RealtimeDrawingApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Model
{
    public class UserModel:IModel
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<ProjectSharedUsersModel> ProjectSharedUsers { get; set; }
    }
}
