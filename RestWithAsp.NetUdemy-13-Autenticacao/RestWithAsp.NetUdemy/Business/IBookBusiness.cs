using RestWithAspNetUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithAspNetUdemy.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long Id);
    }
}
