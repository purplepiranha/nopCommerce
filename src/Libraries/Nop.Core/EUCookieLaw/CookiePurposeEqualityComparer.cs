using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public class CookiePurposeEqualityComparer : IEqualityComparer<ICookiePurpose>
    {
        public bool Equals(ICookiePurpose x, ICookiePurpose y)
        {
            if ((object)x == null && (object)y == null)
            {
                return true;
            }
            if ((object)x == null || (object)y == null)
            {
                return false;
            }
            return x.SystemName == y.SystemName;
        }

        public int GetHashCode([DisallowNull] ICookiePurpose obj)
        {
            return obj.SystemName.GetHashCode();
        }
    }
}
