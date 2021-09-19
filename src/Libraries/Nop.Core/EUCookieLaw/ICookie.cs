using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Core.EUCookieLaw
{
    public interface ICookie
    {
        /// <summary>
        /// When this cookie will expire. 
        /// If null it will expire with session
        /// </summary>
        int? ExpiryHours { get; }
        string Name  { get; }
        string Domain  { get; }
        string Type { get; }

        ICookiePurpose CookiePurpose { get; }
        ICookieProvider CookieProvider { get;  }
    }
}
