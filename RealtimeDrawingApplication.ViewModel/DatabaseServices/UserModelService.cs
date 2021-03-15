using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealtimeDrawingApplication.ViewModel.DatabaseServices
{
    public class UserModelService
    {
        private static Repository<UserModel> database = Repository<UserModel>.GetRepository;

        public static bool SaveToDatabase(UserProxy userProxy)
        {
            UserModel user = new UserModel();
            bool isUserValid = false;
            //var model = Repository<UserModel>.Database.GetUser(userProxy.Email);
            var model = database.GetUser(userProxy.Email);

            if(model == null)
            {
                user = SerializeToUserModel(userProxy);
                //Repository<UserModel>.Database.Create(user);
                database.Create(user);
                isUserValid = true;
            }

            return isUserValid;
        }

        public static UserProxy DeserializeToProxy(string email)
        {
            // UserModel userModel = Repository<UserModel>.Database.GetUser(email);
            UserModel userModel = database.GetUser(email);
            if (userModel == null)
            {
                MessageBox.Show("Invalid Email and Password, Create an Account", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            UserProxy userProxy = new UserProxy();
            userProxy.Email = userModel.Email;
            userProxy.FirstName = userModel.FirstName;
            userProxy.LastName = userModel.LastName;
            userProxy.FullName = userModel.FirstName + " " + userModel.LastName;
            userProxy.Password = userModel.Password;

            return userProxy;
        }

        public static UserProxy DeserializeToProxy(UserModel userModel)
        {
            UserProxy userProxy = new UserProxy();
            if (userModel != null)
            {
                userProxy.Email = userModel.Email;
                userProxy.FirstName = userModel.FirstName;
                userProxy.LastName = userModel.LastName;
                userProxy.FullName = userModel.FirstName + " " + userModel.LastName;
                userProxy.Password = userModel.Password;
            }

            return userProxy;
        }

        public static UserModel SerializeToUserModel(UserProxy userProxy)
        {
            UserModel user = new UserModel();
            user.Email = userProxy.Email;
            user.FirstName = userProxy.FirstName;
            user.LastName = userProxy.LastName;
            user.Password = userProxy.Password;
            return user;
        }

        public static List<ProjectModel> GetProjectList(string email)
        {
            //var projects = Repository<ProjectModel>.Database.GetProjects(email);
            var projects = database.GetProjects(email);
            return projects;
        }
    }
}
