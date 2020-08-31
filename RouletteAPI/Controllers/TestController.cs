using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RouletteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController() 
        {

        }

        [HttpGet("P1")]
        public async Task<ActionResult<string>> P1()
        {
            return Ok("Estao es un prueba 1");
        }
    }
}
