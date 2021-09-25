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
