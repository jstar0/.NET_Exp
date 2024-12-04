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
