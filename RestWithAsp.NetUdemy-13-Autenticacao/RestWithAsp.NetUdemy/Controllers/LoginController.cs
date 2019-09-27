using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Business;
using RestWithAsp.NetUdemy.Data.VO;
using Tapioca.HATEOAS;
using Swashbuckle.Swagger.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAsp.NetUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginService)
        {
            _loginBusiness = loginService;
        }

       
        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody] User user)
        {
            if (user == null) return BadRequest();
            return _loginBusiness.FindByLogin(user);

        }

        
    }
}
