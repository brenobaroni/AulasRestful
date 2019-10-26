using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Context;
using RestWithAspNetUdemy.Repository.Generic;
using RestWithASPNETUdemy.Business;

namespace RestWithAspNetUdemy.Repository.Implementattions
{
    public class PersonRepositoryImplemetattion : GenericRepository<Person>, IPersonRepository
    {

        public PersonRepositoryImplemetattion(MySqlContext context) : base(context) { }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(w => w.FirstName.Equals(firstName) && w.LastName.Equals(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(w => w.FirstName.Equals(firstName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(w => w.LastName.Equals(lastName)).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }

}
