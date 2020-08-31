using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Models.Login.Response
{
    public class PersonResponse
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public int Money { get; set; }
    }
}
