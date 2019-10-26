using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Base;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);

    }
}
