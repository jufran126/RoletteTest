using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models
{
    public class BaseResponse<C> where C : class
    {
        public C Reponse { get; set; }
        public string message { get; set; }
    }
}
