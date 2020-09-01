using RouletteAPI.Data;
using RouletteAPI.Models;
using RouletteAPI.Models.Roulette.Bet.Request;
using RouletteAPI.Models.Roulette.Bet.Response;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.ListRoulette.Response;
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
            try
            {
                NewRouletteResponse response = await DbContex.NewRoulette();
                if (response.id == 0)
                    return new BaseResponse<NewRouletteResponse> { Reponse = null, message = "Can't create roulette" };
                return new BaseResponse<NewRouletteResponse> { Reponse = response };
            }
            catch(Exception ex)
            {
                return new BaseResponse<NewRouletteResponse> { Reponse = null, message = "ERROR: "+ ex.Message };
            }
        }
        public async Task<BaseResponse<CloseResponse>> Close(int id)
        {
            try 
            { 
                CloseResponse response = await DbContex.Close(id);
                if (response.Bet == 0)
                    return new BaseResponse<CloseResponse> { Reponse = null, message = "Not exist roulette" };
                return new BaseResponse<CloseResponse> { Reponse = response };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CloseResponse> { Reponse = null, message = "ERROR: " + ex.Message };
            }
        }
        public async Task<BaseResponse<OpenResponse>> Open(int id)
        {
            try { 
                int response = await DbContex.Open(id);
                if (response == 0)
                    return new BaseResponse<OpenResponse> { Reponse = new OpenResponse { Sucess = false }, message = "false" };
                return new BaseResponse<OpenResponse> { Reponse = new OpenResponse { Sucess = true }, message = "true" };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OpenResponse> { Reponse = null, message = "ERROR: " + ex.Message };
            }
        }
        public async Task<BaseResponse<List<RouletteResponse>>> ListRoulettes()
        {
            try
            {
                List<RouletteResponse> responses = await DbContex.ListRoulettes();
                if (responses.Count == 0)
                    return new BaseResponse<List<RouletteResponse>> { Reponse = null, message = "Not exist list" };
                return new BaseResponse<List<RouletteResponse>> { Reponse = responses };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<RouletteResponse>> { Reponse = null, message = "ERROR: " + ex.Message };
            }
        }
        public async Task<BaseResponse<BetResponse>> Bet(BetAppRequest request)
        {
            try
            {
                if (await DbContex.VRoulette(request.bet.idRolette))
                {
                    int Money = await DbContex.GetMoney(request.idUser);
                    if (request.bet.BetValue <= 10000 && request.bet.BetValue <= Money)
                    {
                        Money -= request.bet.BetValue;
                        DbContex.addMoney(request.idUser, Money);
                        Random rnd = new Random();
                        int numb = rnd.Next(36);
                        if (!string.IsNullOrEmpty(request.bet.Color))
                        {
                            if (numb % 2 == 0) 
                            {
                                if (request.bet.Color == "red")
                                {
                                    DbContex.addBet(request.bet.idRolette, request.bet.BetValue);
                                    Money += (2 * request.bet.BetValue);
                                    DbContex.addMoney(request.idUser, Money);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = true } };
                                }
                                else if (request.bet.Color == "black")
                                {
                                    DbContex.addBet(request.bet.idRolette, request.bet.BetValue);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = false } };
                                }
                                else 
                                {
                                    Money += request.bet.BetValue;
                                    DbContex.addMoney(request.idUser, Money);
                                    return new BaseResponse<BetResponse> { Reponse = null, message = "Color not validated" };
                                }
                            }
                            else
                            {
                                if (request.bet.Color == "black")
                                {
                                    DbContex.addBet(request.bet.idRolette, request.bet.BetValue);
                                    Money += (2 * request.bet.BetValue);
                                    DbContex.addMoney(request.idUser, Money);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = true } };
                                }
                                else if (request.bet.Color == "red")
                                {
                                    DbContex.addBet(request.bet.idRolette, request.bet.BetValue);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = false } };
                                }
                                else
                                {
                                    Money += request.bet.BetValue;
                                    DbContex.addMoney(request.idUser, Money);
                                    return new BaseResponse<BetResponse> { Reponse = null, message = "Color not validated" };
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(request.bet.Number))
                        {
                            if (Convert.ToInt32(request.bet.Number) <= 36 && Convert.ToInt32(request.bet.Number) >= 0)
                            {
                                if (Convert.ToInt32(request.bet.Number) == numb)
                                {
                                    Money += (35 * request.bet.BetValue);
                                    DbContex.addMoney(request.idUser, Money);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = true } };
                                }
                                else
                                {
                                    DbContex.addBet(request.bet.idRolette, request.bet.BetValue);
                                    return new BaseResponse<BetResponse> { Reponse = new BetResponse { Win = false } };
                                }
                            }
                            else
                            {
                                Money += request.bet.BetValue;
                                DbContex.addMoney(request.idUser, Money);
                                return new BaseResponse<BetResponse> { Reponse = null, message = "Number not validated" };
                            }
                        }
                        Money += request.bet.BetValue;
                        DbContex.addMoney(request.idUser, Money);
                        return new BaseResponse<BetResponse> { Reponse = null, message = "No bet" };                        
                    }
                    else
                        return new BaseResponse<BetResponse> { Reponse = null, message = "Value of bet not validated" };
                }
                else
                    return new BaseResponse<BetResponse> { Reponse = null, message = "Rolette close" };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BetResponse> { Reponse = null, message = "ERROR: " + ex.Message };
            }
        }
        #endregion
    }
}
