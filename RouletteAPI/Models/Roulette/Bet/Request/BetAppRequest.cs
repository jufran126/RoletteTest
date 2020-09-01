using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models.Roulette.Bet.Request
{
    public class BetAppRequest
    {
        public BetRequest bet { get; set; }
        public int idUser { get; set; }
    }
}
