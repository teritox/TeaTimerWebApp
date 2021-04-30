using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeaTimer.Models
{
    public class Tea
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Sort { get; set; }
        public string Temperature { get; set; }
        public string BrewTime { get; set; }
    }
}
