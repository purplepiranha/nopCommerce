namespace Nop.Core.EUCookieLaw
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
