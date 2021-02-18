using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.Infrastructure
{
    public interface IRepository<T>
    {
        void Create(T model);
        T GetModel(int id);
        void Update(T model);
        void Update(int id, T model);
        void Delete(T model);
    }
}
