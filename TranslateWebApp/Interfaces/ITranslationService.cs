namespace TranslateWebApp.Interfaces
{
    public interface ITranslationService
    {
        Task<string> GetServiceInfoAsync();
        Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en");
    }
}
