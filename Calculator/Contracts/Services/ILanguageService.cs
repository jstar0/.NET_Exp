using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Contracts.Services;
public interface ILanguageService
{
    public enum Lang
    {
        Default,
        English,
        Chinese
    }

    Task InitializeAsync();

    Task SetLanguageAsync(Lang language);

    Task SetRequestedLanguageAsync();

    public Task<Lang> LoadLanguageFromSettingsAsync();
}
