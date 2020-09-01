using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.ListRoulette.Response;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using RouletteAPI.Models.Roulette.Open.Response;
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
        [HttpGet("New")]
        public async Task<ActionResult<BaseResponse<NewRouletteResponse>>> NewRoulette()
        {
            BaseResponse<NewRouletteResponse> response = await _rouletteService.NewRoulete();
            if (!string.IsNullOrEmpty(response.message))
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("Close")]
        public async Task<ActionResult<BaseResponse<CloseResponse>>> Close(int id)
        {
            BaseResponse<CloseResponse> response = await _rouletteService.Close(id);
            if (!string.IsNullOrEmpty(response.message))
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("Open")]
        public async Task<ActionResult<BaseResponse<OpenResponse>>> Open(int id)
        {
            BaseResponse<OpenResponse> response = await _rouletteService.Open(id);
            if (!string.IsNullOrEmpty(response.message))
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("List")]
        public async Task<ActionResult<BaseResponse<List<RouletteResponse>>>> ListRoulettes()
        {
            BaseResponse<List<RouletteResponse>> response = await _rouletteService.ListRoulettes();
            if (!string.IsNullOrEmpty(response.message))
                return BadRequest(response);
            return Ok(response);
        }
        #endregion
    }
}
