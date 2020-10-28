using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Models
{
    public class TokenModel
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }

    }
}
