using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAsp.NetUdemy.Data.Converters;
using RestWithAsp.NetUdemy.Data.VO;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithAsp.NetUdemy.Repository;
using RestWithAsp.NetUdemy.Repository.Generic;

namespace RestWithAsp.NetUdemy.Business.Implementattions
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }


        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

    }
}
