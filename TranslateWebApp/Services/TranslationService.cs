using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IExternalTranslationApi _translationApi;
        private readonly ITranslationCache _cache;

        public TranslationService(IExternalTranslationApi translationApi, ITranslationCache cache)
        {
            _translationApi = translationApi;
            _cache = cache;
        }

        public async Task<string> GetServiceInfoAsync()
        {
            return $"Service: {_translationApi.GetServiceName()}, Cache: {_cache.GetCacheType()} with {_cache.GetCacheSize()} entries";
        }

        public async Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en")
        {
            List<string> translations = await _translationApi.TranslateAsync(inputStrings, targetLanguage, sourceLanguage);

            // Cashing
            _cache.AddTranslation(inputStrings, sourceLanguage, targetLanguage, translations);

            return translations;
        }
    }
}
