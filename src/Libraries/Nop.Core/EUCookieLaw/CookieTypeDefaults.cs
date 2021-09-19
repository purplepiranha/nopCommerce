using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public static partial class CookieTypeDefaults
    {
        public static string Cookie => "EUCookieLaw.CookieTypes.Cookie";
        public static string LocalStorage => "EUCookieLaw.CookieTypes.LocalStorage";
        public static string SessionStorage => "EUCookieLaw.CookieTypes.SessionStorage";
    }
}
