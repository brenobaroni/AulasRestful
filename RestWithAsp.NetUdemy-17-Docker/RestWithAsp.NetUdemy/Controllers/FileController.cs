using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;
using Google.Protobuf.WellKnownTypes;
using Swashbuckle.Swagger.Annotations;
namespace RestWithAspNetUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class FileController : Controller
    {
        private IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileService)
        {
            _fileBusiness = fileService;
        }


        [HttpGet]
        [SwaggerResponse((200), Type = typeof(byte[]))]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        [SwaggerResponse(401)]
        [Authorize("Bearer")]
        public IActionResult GetPDFFile()
        {
            byte[] buffer = _fileBusiness.GetPDFFiles();
            if(buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }



    }
}
