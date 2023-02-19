using System.Globalization;

namespace SocionicTeamBuilder.Mobile.Droid
{
    public class Localize : Mobile.ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return new CultureInfo(netLanguage);
        }
    }
}