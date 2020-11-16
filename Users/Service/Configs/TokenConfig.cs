using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Configs
{
    public class TokenConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
