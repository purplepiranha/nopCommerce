namespace Nop.Core.EUCookieLaw
{
    public class MarketingCookiePurpose : ICookiePurpose
    {

        public string SystemName => "EUCookieLaw.Purposes.Marketing";
        public string TitleResourceKey => $"{ SystemName }.Title";
        public string DescriptionResourceKey => $"{ SystemName }.Description";
        public bool IsNecessary => false;
        public int Order => 400;
    }
}
