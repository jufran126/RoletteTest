using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models.Roulette.Bet.Request
{
    public class BetRequest
    {
        public string Color { get; set; }
        public string Number { get; set; }
        public int BetValue { get; set; }
        public int idRolette { get; set; }
    }
}
