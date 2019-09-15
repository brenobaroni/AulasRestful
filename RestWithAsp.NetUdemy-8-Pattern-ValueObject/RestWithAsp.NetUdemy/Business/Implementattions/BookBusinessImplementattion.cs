using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithAsp.NetUdemy.Repository;
using RestWithAsp.NetUdemy.Repository.Generic;

namespace RestWithAsp.NetUdemy.Business.Implementattions
{
    public class BookBusinessImplementation : IBookBusiness
    {

        private IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }


        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

    }
}
