using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lacuna_genetics
{
    public class DecodeStrandBody
    {
        public string Strand { get; set; }

        public DecodeStrandBody() {}
    }

    public class DecodeStrandResponse
    {
        public string Code { get; set; }
        public string? Message { get; set; }
    }
}
