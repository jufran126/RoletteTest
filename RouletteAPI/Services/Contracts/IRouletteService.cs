using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.ListRoulette.Response;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using RouletteAPI.Models.Roulette.Open.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Services.Contracts
{
    public interface IRouletteService
    {
        Task<BaseResponse<NewRouletteResponse>> NewRoulete();
        Task<BaseResponse<CloseResponse>> Close(int id);
        Task<BaseResponse<OpenResponse>> Open(int id);
        Task<BaseResponse<List<RouletteResponse>>> ListRoulettes();
    }
}
