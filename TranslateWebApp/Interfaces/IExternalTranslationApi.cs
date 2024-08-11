namespace TranslateWebApp.Interfaces
{
    public interface IExternalTranslationApi
    {
        string GetServiceName();
        Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en");
    }
}
