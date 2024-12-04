using static Calculator.Contracts.Services.ILanguageService;

namespace Calculator.Helpers;
public static class LanguageChangeHelper
{
    public static void ChangeLanguage(Lang language)
    {
        // 更改语言
        if (language == Lang.English)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-us";
        }
        else if (language == Lang.Chinese)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "zh-cn";
        }
        else
        {
            throw new InvalidDataException();
        }
    }
}
