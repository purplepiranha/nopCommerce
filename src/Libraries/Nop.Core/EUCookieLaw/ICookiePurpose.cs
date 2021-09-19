using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public interface ICookiePurpose
    {
        string SystemName { get; }
        string TitleResourceKey { get; }
        string DescriptionResourceKey { get; }
        bool IsNecessary { get; }
        int Order { get; }
    }
}
