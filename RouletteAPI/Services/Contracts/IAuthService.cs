using RouletteAPI.Models;
using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<BaseResponse<PersonResponse>> Login(LoginRequest request);
    }
}
