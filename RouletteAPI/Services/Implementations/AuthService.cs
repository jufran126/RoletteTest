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
        IDbContex DbContex;
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
            /*PersonResponse person = new PersonResponse() { 
                Name="Juan Francisco",
                id=1,
                User="jf"
            };*/
            PersonResponse person = await DbContex.Person(request);
            if (person == null)
                return new BaseResponse<PersonResponse> { Reponse = null, message = "Not exists the user" };
            return new BaseResponse<PersonResponse> { Reponse = person };            
        }
        #endregion
    }
}
