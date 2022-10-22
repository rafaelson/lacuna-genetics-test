using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lacuna_genetics
{
    public class EncodeStrandBody
    {
        public string StrandEncoded { get; set; }
        public EncodeStrandBody() {} 
    }

    public class EncodeStrandResponse
    {
        public string Code { get; set; }
        public string? Message { get; set; }
    }
}
