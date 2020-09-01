using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.ListRoulette.Response;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Data
{
    public interface IDbContex
    {
        Task<PersonResponse> GetPerson(LoginRequest request);
        Task<NewRouletteResponse> NewRoulette();
        Task<CloseResponse> Close(int id);
        Task<int> Open(int id);
        Task<List<RouletteResponse>> ListRoulettes();
    }
}
