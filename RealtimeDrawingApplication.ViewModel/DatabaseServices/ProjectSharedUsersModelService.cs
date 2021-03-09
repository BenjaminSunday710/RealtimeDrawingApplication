using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.DatabaseServices
{
    public class ProjectSharedUsersModelService
    {
        public static void SaveToDatabase(ProjectSharedUserProxy projectSharedUsersProxy)
        {
            ProjectSharedUsersModel projectSharedUser = new ProjectSharedUsersModel();
            projectSharedUser.Project = projectSharedUsersProxy.Project;
            projectSharedUser.SharedUser = projectSharedUsersProxy.ProjectSharedUser;
            projectSharedUser.IsEditable = projectSharedUsersProxy.IsEditable;
            projectSharedUser.IsAllowed = projectSharedUsersProxy.IsAllowed;

            Repository<ProjectSharedUsersModel>.Database.Create(projectSharedUser);
        }

        public static List<ProjectSharedUserProxy> DeserializeToProxy(string projectName)
        {
            List<ProjectSharedUsersModel> sharedUsers = Repository<ProjectSharedUsersModel>.Database.GetProjectSharedUsers(projectName);
            List<ProjectSharedUserProxy> sharedUsersProxies = new List<ProjectSharedUserProxy>();
            ProjectSharedUserProxy sharedUserProxy = new ProjectSharedUserProxy();
           
            if (sharedUsers != null)
            {
                foreach (var sharedUser in sharedUsers)
                {
                    sharedUserProxy.ProjectSharedUser = sharedUser.SharedUser;
                    sharedUserProxy.Project = sharedUser.Project;
                    sharedUserProxy.IsAllowed = sharedUser.IsAllowed;
                    sharedUserProxy.IsEditable = sharedUser.IsEditable;

                    sharedUsersProxies.Add(sharedUserProxy);
                }
            }
            return sharedUsersProxies;
        }

        public static ProjectSharedUserProxy DeserializeToProxy(ProjectSharedUsersModel sharedUser)
        {
            ProjectSharedUserProxy sharedUserProxy = new ProjectSharedUserProxy();

            if (sharedUser != null)
            {
                sharedUserProxy.ProjectSharedUser = sharedUser.SharedUser;
                sharedUserProxy.Project = sharedUser.Project;
                sharedUserProxy.IsAllowed = sharedUser.IsAllowed;
                sharedUserProxy.IsEditable = sharedUser.IsEditable;
            }
            return sharedUserProxy;
        }
    }
}
