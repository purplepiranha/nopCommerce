namespace Nop.Core.EUCookieLaw
{
    public class AnalyticalCookiePurpose :  ICookiePurpose
    {
        public string SystemName => "EUCookieLaw.Purposes.Analytical";
        public string TitleResourceKey => $"{ SystemName }.Title";
        public string DescriptionResourceKey => $"{ SystemName }.Description";
        public bool IsNecessary => false;
        public int Order => 300;
    }
}
