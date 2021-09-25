namespace Nop.Core.EUCookieLaw
{
    public class PreferenceCookiePurpose :  ICookiePurpose
    {
        public string SystemName => "EUCookieLaw.Purposes.Preference";
        public string TitleResourceKey => $"{ SystemName }.Title";
        public string DescriptionResourceKey => $"{ SystemName }.Description";
        public bool IsNecessary => false;
        public int Order => 200;
    }
}
