using RouletteAPI.Data;
using RouletteAPI.Models;
using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using RouletteAPI.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Services.Implementations
{
    public class AuthService:IAuthService
    {
        #region Properties
        private IDbContex DbContex;
        #endregion
        #region Constructor
        public AuthService(IDbContex dbContex)
        {
            DbContex = dbContex;
        }
        #endregion
        #region Methods
        public async Task<BaseResponse<PersonResponse>> Login(LoginRequest request)
        {
            PersonResponse person = await DbContex.GetPerson(request);
            if (person == null)
                return new BaseResponse<PersonResponse> { Reponse = null, message = "Not exists the user" };
            return new BaseResponse<PersonResponse> { Reponse = person };            
        }
        #endregion
    }
}
