using RestWithAsp.NetUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAsp.NetUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);


    }
}
