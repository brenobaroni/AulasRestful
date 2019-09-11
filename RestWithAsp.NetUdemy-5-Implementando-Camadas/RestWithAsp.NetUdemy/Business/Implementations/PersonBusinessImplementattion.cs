using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithAsp.NetUdemy.Repository;

namespace RestWithAsp.NetUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }



        // Metodo responsável por criar uma nova pessoa
        // Se tivéssemos um banco de dados esse seria o
        // momento de persistir os dados
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }


        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }


        // Método responsável por retornar uma pessoa
        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

    }
}
