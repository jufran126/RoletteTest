using RouletteAPI.Data;
using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
using RouletteAPI.Models.Roulette.Open.Response;
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
                return new BaseResponse<NewRouletteResponse> { Reponse = null, message = "Can't create roulette" };
            return new BaseResponse<NewRouletteResponse> { Reponse = response };
        }
        public async Task<BaseResponse<CloseResponse>> Close(int id)
        {
            CloseResponse response = await DbContex.Close(id);
            if (response.Bet == 0)
                return new BaseResponse<CloseResponse> { Reponse = null, message = "Not exist roulette" };
            return new BaseResponse<CloseResponse> { Reponse = response };
        }
        public async Task<BaseResponse<OpenResponse>> Open(int id)
        {
            int response = await DbContex.Open(id);
            if (response == 0)
                return new BaseResponse<OpenResponse> { Reponse = new OpenResponse { Sucess = false }, message = "false" };
            return new BaseResponse<OpenResponse> { Reponse = new OpenResponse { Sucess = true }, message = "true" };
        }
        #endregion
    }
}
