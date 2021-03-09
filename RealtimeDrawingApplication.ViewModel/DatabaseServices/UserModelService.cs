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
       // private static Repository<UserModel> database = new Repository<UserModel>();

        public static bool SaveToDatabase(UserProxy userProxy)
        {
            UserModel user = new UserModel();
            bool isEmailValid = false;
            var model = Repository<UserModel>.Database.GetUser(userProxy.Email);

            if(model == null)
            {
                user.Email = userProxy.Email;
                user.FirstName = userProxy.FirstName;
                user.LastName = userProxy.LastName;
                user.Password = userProxy.Password;
                Repository<UserModel>.Database.Create(user);
                isEmailValid = true;
            }

            return isEmailValid;
        }

        public static UserProxy DeserializeToProxy(string email)
        {
            UserModel userModel = Repository<UserModel>.Database.GetUser(email);
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

        public static List<ProjectModel> GetProjectList(string email)
        {
            var projects = Repository<ProjectModel>.Database.GetProjects(email);
            return projects;
        }
    }
}
