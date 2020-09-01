using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using RouletteAPI.Services.Contracts;

namespace RouletteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        #region Properties
        private readonly IRouletteService _rouletteService;
        #endregion
        #region Constructor
        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        #endregion
        #region Methods
        [HttpGet("NewRoulette")]
        public async Task<ActionResult<BaseResponse<NewRouletteResponse>>> NewRoulette()
        {
            BaseResponse<NewRouletteResponse> response = await _rouletteService.NewRoulete();
            if (!string.IsNullOrEmpty(response.message))
                return BadRequest(response);
            return Ok(response);
        }
        #endregion
    }
}
