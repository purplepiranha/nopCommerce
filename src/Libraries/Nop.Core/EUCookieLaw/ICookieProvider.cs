using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public interface ICookieProvider
    {
        string SystemName { get; }
        string Name { get; }
        string Description { get; }
        string PrivacyPolicyUrl { get; }
        ICookiePurpose CookiePurpose { get; }
        int Order { get; }
        string[] CookieNames { get; }

        Task<bool> IsActiveAsync();
    }
}
