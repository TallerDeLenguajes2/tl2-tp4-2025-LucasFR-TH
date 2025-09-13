using Microsoft.AspNetCore.Mvc;

namespace P4WebAPI.Controllers
{
    [ApiController]
    [Route("api/cadeteria")]

    public class CadeteriaController : ControllerBase

    {
        public CadeteriaController()
        {
        }

        [HttpGet]
         public string holamundo()
         {
             return "Hola mundo";
        }
    }
}
