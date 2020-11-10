using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BaseResponse
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string total { get; set; }
        public string token { get; set; }
    }
}
