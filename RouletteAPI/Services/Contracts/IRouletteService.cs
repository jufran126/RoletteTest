using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Services.Contracts
{
    public interface IRouletteService
    {
        Task<BaseResponse<NewRouletteResponse>> NewRoulete();
    }
}
