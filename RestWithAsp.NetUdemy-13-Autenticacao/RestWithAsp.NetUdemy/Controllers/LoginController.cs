using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;

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
        public object Post([FromBody] UserVO user)
        {
            if (user == null) return BadRequest();
            return _loginBusiness.FindByLogin(user);

        }

        
    }
}
