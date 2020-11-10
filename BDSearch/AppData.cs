using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BDSearch
{
    public static class AppData
    {
        /// <summary>
     /// 父级窗体
     /// </summary>
        public static Window ParentWindow;

        /// <summary>
        /// 登陆Token
        /// </summary>
        public static string Token { get; set; }
    }
}
