using RestWithAsp.NetUdemy.Model.Base;
using RestWithAsp.NetUdemy.Model.Context;
using System.Collections.Generic;

namespace RestWithAsp.NetUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long Id);

        bool Exists(long? id);
    }
}
