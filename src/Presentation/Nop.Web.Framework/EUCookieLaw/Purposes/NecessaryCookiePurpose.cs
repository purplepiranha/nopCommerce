using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Services.EUCookieLaw;

namespace Nop.Web.Framework.EUCookieLaw.Purposes
{
    public class NecessaryCookiePurpose : ICookiePurpose
    {

        public string SystemName => "EUCookieLaw.Purposes.Necessary";
        public string TitleResourceKey => $"{ SystemName }.Title";
        public string DescriptionResourceKey => $"{ SystemName }.Description";
        public bool IsNecessary => true;
        public int Order => 100;
    }
}
