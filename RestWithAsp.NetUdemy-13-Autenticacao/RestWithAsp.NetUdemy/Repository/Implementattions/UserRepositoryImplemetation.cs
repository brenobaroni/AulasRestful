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
    public class UserRepositoryImplemetation : IUserRepository
    {

        private MySqlContext _context;

        public UserRepositoryImplemetation(MySqlContext context)
        {
            _context = context;
        }
        // Método responsável por retornar uma pessoa
        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(p => p.Login.Equals(login));
        }
    }

}
