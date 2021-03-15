using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.DatabaseServices
{
    public class ProjectModelService
    {
        private static Repository<ProjectModel> database = Repository<ProjectModel>.GetRepository;
        public static void SaveToDatabase(ProjectProxy projectProxy)
        {
            ProjectModel project = new ProjectModel();
            project.Name = projectProxy.Name;
            project.ProjectCreator = projectProxy.ProjectCreator;
            project.ProjectSharedUsers = projectProxy.ProjectSharedUsers;
            project.DrawingComponents = projectProxy.DrawingComponents;
            database.Create(project);
            //Repository<ProjectModel>.Database.Create(project);
        }

        public static ProjectProxy DeserializeToProxy(string name)
        {
            //ProjectModel project = Repository<ProjectModel>.Database.GetProject(name);
            ProjectModel project = database.GetProject(name);
            ProjectProxy projectProxy = new ProjectProxy();
            if (project != null)
            {
                projectProxy.Name = project.Name;
                projectProxy.ProjectCreator = project.ProjectCreator;
                projectProxy.ProjectSharedUsers = project.ProjectSharedUsers;
                projectProxy.DrawingComponents = project.DrawingComponents;
            }
            return projectProxy;
        }

        public static ProjectProxy DeserializeToProxy(ProjectModel projectModel)
        {
            ProjectProxy projectProxy = new ProjectProxy();
            if (projectModel != null)
            {
                projectProxy.Name = projectModel.Name;
                projectProxy.ProjectCreator = projectModel.ProjectCreator;
                projectProxy.ProjectSharedUsers = projectModel.ProjectSharedUsers;
                projectProxy.DrawingComponents = projectModel.DrawingComponents;
            }
            return projectProxy;
        }

        public static List<ProjectSharedUsersModel> GetSharedUsers(string projectName)
        {
            var sharedUsers = database.GetProjectSharedUsers(projectName);
            //var sharedUsers = Repository<ProjectSharedUsersModel>.Database.GetProjectSharedUsers(projectName);
            return sharedUsers;
        }
    }
}
