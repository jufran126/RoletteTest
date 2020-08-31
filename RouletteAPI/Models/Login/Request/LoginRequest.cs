using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models.Login.Request
{
    public class LoginRequest
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }
}
