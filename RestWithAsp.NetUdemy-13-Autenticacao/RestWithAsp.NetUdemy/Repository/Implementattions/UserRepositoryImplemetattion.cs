using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Context;
using RestWithASPNETUdemy.Business;

namespace RestWithAspNetUdemy.Repository.Implementattions
{
    public class UserRepositoryImplemetattion : IUserRepository
    {

        private MySqlContext _context;

        public UserRepositoryImplemetattion(MySqlContext context)
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
