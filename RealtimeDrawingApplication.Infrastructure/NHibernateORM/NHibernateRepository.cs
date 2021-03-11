using NHibernate;
using NHibernate.Criterion;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure.NHibernateORM
{
    public class NHibernateRepository<T>:IRepository<T>
    {
        public ISessionFactory SessionFactory { get; private set; }

        public NHibernateRepository(IDatabaseType databaseType)
        {
            SessionFactory = databaseType.CreateSessionFactory();
        }

        public void Create(T model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(model);
                    transaction.Commit();
                }
            }
        }

        public void Delete(T model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(model);
                    transaction.Commit();
                }
            }
        }

        public T GetModel(int id)
        {
            T model;
            using (ISession session = SessionFactory.OpenSession())
            {
                model = session.Get<T>(id);
            }
            return model;
        }

        public void Update(T model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(model);
                    transaction.Commit();
                }
            }
        }

        public void Update(int id, T model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    T prevModel = session.Get<T>(id);
                    prevModel = model;
                    session.Update(model);
                    transaction.Commit();
                }
            }
        }

        public UserModel GetUser(string email)
        {
            UserModel user;

            using (var session=SessionFactory.OpenSession())
            {
                user = session.Query<UserModel>().FirstOrDefault(x => x.Email == email);
            }

            return user;
        }

        public ProjectModel GetProject(string name)
        {
            ProjectModel project;

            using (var session = SessionFactory.OpenSession())
            {
                project = session.Query<ProjectModel>().FirstOrDefault(x => x.Name == name);
            }

            return project;
        }

        public List<DrawingComponentModel> GetDrawingComponents(string projectName)
        {
            List<DrawingComponentModel> drawingComponents = new List<DrawingComponentModel>();

            using (var session=SessionFactory.OpenSession())
            {
                var project = session.Query<ProjectModel>().FirstOrDefault(x => x.Name == projectName);
                //int projectId=
                drawingComponents = session.Query<DrawingComponentModel>().Where(x => x.Project.Id == project.Id).ToList();
            }

            return drawingComponents;
        }

        public List<ProjectSharedUsersModel> GetProjectSharedUsers(string projectName)
        {
            List<ProjectSharedUsersModel> projectSharedUsers = new List<ProjectSharedUsersModel>();

            using (var session = SessionFactory.OpenSession())
            {
                var project = session.Query<ProjectModel>().FirstOrDefault(x => x.Name == projectName);
                projectSharedUsers = session.Query<ProjectSharedUsersModel>().Where(x => x.Project == project).ToList();
            }

            return projectSharedUsers;
        }

        public List<ProjectModel> GetProjects(string userEmail)
        {
            List<ProjectModel> projects = new List<ProjectModel>();

            using (var session = SessionFactory.OpenSession())
            {
                var user = session.Query<UserModel>().FirstOrDefault(x => x.Email == userEmail);
                projects = session.Query<ProjectModel>().Where(x => x.ProjectCreator == user).ToList();
            }

            return projects;
        }
    }
}
