using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UpdateSourceResponse : BaseResponse
    {
        public ShareLinkInfo plist { get; set; }
    }

    public class ShareLinkInfo
    {
        public string id { get; set; }
        public string ss { get; set; }
        public string link { get; set; }
        public string code { get; set; }
        public string score { get; set; }
        public string status { get; set; }
    }
}
