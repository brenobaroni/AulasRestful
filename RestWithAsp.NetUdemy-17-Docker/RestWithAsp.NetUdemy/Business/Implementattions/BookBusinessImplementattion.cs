﻿using System.Collections.Generic;
using RestWithAspNetUdemy.Data.Converters;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Repository.Generic;

namespace RestWithAspNetUdemy.Business.Implementattions
{
    public class BookBusinessImplementattion : IBookBusiness
    {

        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementattion(IRepository<Book> repository)
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
