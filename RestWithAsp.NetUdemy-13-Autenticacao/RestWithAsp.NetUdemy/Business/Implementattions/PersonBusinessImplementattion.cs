using System.Collections.Generic;
using RestWithAspNetUdemy.Data.Converters;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Repository.Generic;

namespace RestWithAspNetUdemy.Business.Implementattions
{
    public class PersonBusinessImplementattion : IPersonBusiness
    {

        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementattion(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        // Metodo responsável por criar uma nova pessoa
        // Se tivéssemos um banco de dados esse seria o
        // momento de persistir os dados
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        // Método responsável por retornar todas as pessoas
        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        // Método responsável por retornar uma pessoa
        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        // Método responsável por atualizar uma pessoa
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

    }
}
