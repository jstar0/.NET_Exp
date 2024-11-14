using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Contracts.Services;
using Calculator.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using static Calculator.Contracts.Services.ILanguageService;

namespace Calculator.Services;
public class LanguageService : ILanguageService
{
    private const string SettingsKey = "AppLanguage";

    private readonly ILocalSettingsService _localSettingsService;

    public LanguageService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public Lang Language { get; set; } = Lang.Default;

    public async Task InitializeAsync()
    {
        Language = await LoadLanguageFromSettingsAsync();
        await Task.CompletedTask;
    }

    public async Task SetLanguageAsync(Lang language)
    {
        Language = language;

        await SaveLanguageInSettingsAsync(Language);
        await SetRequestedLanguageAsync();
    }

    public async Task SetRequestedLanguageAsync()
    {
        // 对于 Default 则不做更改，对于其他语言则设置
        if (Language != Lang.Default)
        {
            // 设置语言
            LanguageChangeHelper.ChangeLanguage(Language);
        }

        // 更新应用程序的标题
        App.MainWindow.Title = "AppDisplayName".GetLocalized();

        await Task.CompletedTask;
    }

    private async Task SaveLanguageInSettingsAsync(Lang language)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, language.ToString());
    }

    public async Task<Lang> LoadLanguageFromSettingsAsync()
    {
        var languageName = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);
        if (Enum.TryParse(languageName, out Lang cacheLanguage))
        {
            return cacheLanguage;
        }
        return Lang.Default;
    }
}
