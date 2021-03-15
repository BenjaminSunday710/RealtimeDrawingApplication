using RealtimeDrawingApplication.Common;
using RealtimeDrawingApplication.Infrastructure;
using RealtimeDrawingApplication.Infrastructure.NHibernateORM;
using RealtimeDrawingApplication.Infrastructure.NHibernateORM.DatabaseType;
using RealtimeDrawingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.DatabaseServices
{
    public class Repository<T>:NHibernateRepository<T>
    {
        private static IDatabaseType sqLiteDatabase=new SqLiteSessionFactoryCreator();
        //private static NHibernateRepository<T> database = new NHibernateRepository<T>(sqLiteDatabase);
        private static Repository<T> _repository = null;
       
        private Repository():base(sqLiteDatabase) { }

        //public static NHibernateRepository<T> Database { get => database; }
        public static Repository<T> GetRepository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new Repository<T>();
                }
                return _repository;
            }
            private set { }
        }

        //public void CreateModel(T model)
        //{
        //    database.Create(model);
        //}

        //public T GetModel(int id)
        //{
        //    T model = database.GetModel(id);
        //    return model;
        //}

        //public UserModel GetUser(string Email)
        //{
        //    var model = database.GetUser(Email);
        //    return model;
        //}

        //public ProjectModel GetProject(string name)
        //{
        //    var model = database.GetProject(name);
        //    return model;
        //}

        //public void UpdateModel(int id, T currentModel)
        //{
        //    database.Update(id, currentModel);
        //}

        //public void Delete(T model)
        //{
        //    database.Delete(model);
        //}

       

    }
}
