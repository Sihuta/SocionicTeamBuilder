using System.Globalization;

namespace SocionicTeamBuilder.Mobile
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            //var androidLocale = Java.Util.Locale.Default;
            //var netLanguage = androidLocale.ToString().Replace("_", "-");
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return new CultureInfo(lang);
        }
    }
}