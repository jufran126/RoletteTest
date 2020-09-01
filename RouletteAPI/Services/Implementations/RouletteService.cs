using RouletteAPI.Data;
using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using RouletteAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Services.Implementations
{
    public class RouletteService:IRouletteService
    {
        #region Properties
        private IDbContex DbContex;
        #endregion
        #region Constructor
        public RouletteService(IDbContex dbContex)
        {
            DbContex = dbContex;
        }
        #endregion
        #region Methods
        public async Task<BaseResponse<NewRouletteResponse>> NewRoulete()
        {
            NewRouletteResponse response = await DbContex.NewRoulette();
            if (response.id == 0)
                return new BaseResponse<NewRouletteResponse> { Reponse = null, message = "Can't create rouletta" };
            return new BaseResponse<NewRouletteResponse> { Reponse = response };
        }
        #endregion
    }
}
