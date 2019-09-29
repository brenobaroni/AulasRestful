using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personService)
        {
            _personBusiness = personService;
        }

        // GET api/values
        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        [SwaggerResponse((201), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));

        }

        // PUT api/values/5
        [HttpPut]
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            PersonVO updateperson = _personBusiness.Update(person);
            if (updateperson == null) return NoContent();
            return new ObjectResult(updateperson);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
