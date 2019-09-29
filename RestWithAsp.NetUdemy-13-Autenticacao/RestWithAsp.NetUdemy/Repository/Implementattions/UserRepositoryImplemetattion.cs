using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithASPNETUdemy.Business;

namespace RestWithAsp.NetUdemy.Repository.Implementattions
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
