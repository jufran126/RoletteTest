using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Data
{
    public interface IDbContex
    {
        Task<PersonResponse> GetPerson(LoginRequest request);
    }
}
