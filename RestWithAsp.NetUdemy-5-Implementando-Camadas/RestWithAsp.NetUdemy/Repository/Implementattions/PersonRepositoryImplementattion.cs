using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Model.Context;

namespace RestWithAsp.NetUdemy.Repository.Implementattions
{
    public class PersonRepositoryImplementattion : IPersonRepository
    {

        private MySqlContext _context;

        public PersonRepositoryImplementattion(MySqlContext context)
        {
            _context = context;
        }



        // Metodo responsável por criar uma nova pessoa
        // Se tivéssemos um banco de dados esse seria o
        // momento de persistir os dados
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return person;
        }


        // Método responsável por deletar
        // uma pessoa a partir de um ID
        public void Delete(long Id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(Id));

            try
            {
                if(result != null)
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Método responsável por retornar todas as pessoas
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }


        // Método responsável por retornar uma pessoa
        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id == id);
        }

        // Método responsável por atualizar uma pessoa
        public Person Update(Person person)
        {
            //Caso não exista retonra nulo.
            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return person;
        }

        //Método responsável por confirmar a existencia da pessoa.
        public bool Exists(long? id)
        {
            var result = _context.Persons.Where(p => p.Id.Equals(id)).Count();
            bool exists;
            if (result > 0)
                exists = true;
            else
                exists = false;

            return exists;
        }
    }
}
