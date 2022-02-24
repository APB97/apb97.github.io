using apb97.github.io.Shared;
using Microsoft.AspNetCore.Mvc;

namespace apb97.github.io.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegerToRomanController : ControllerBase
    {
        public IntegerToRomanController()
        {
        }

        [HttpGet]
        [Route("[controller]/IntegerToRoman")]
        public IActionResult IntegerToRoman()
        {
            return RedirectToPage("IntegerToRoman");
        }
    }
}