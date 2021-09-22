using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Services.EUCookieLaw;

namespace Nop.Web.Framework.EUCookieLaw.Purposes
{
    public class AnalyticalCookiePurpose :  ICookiePurpose
    {
        public string SystemName => "EUCookieLaw.Purposes.Analytical";
        public string TitleResourceKey => $"{ SystemName }.Title";
        public string DescriptionResourceKey => $"{ SystemName }.Description";
        public bool IsNecessary => false;
        public int Order => 200;
    }
}
