using RestWithAspNetUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        List<PersonVO> FindByName(string fistName, string lastName);
        PersonVO Update(PersonVO person);
        void Delete(long Id);
    }
}
