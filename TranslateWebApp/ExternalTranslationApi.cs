using Google.Cloud.Translation.V2;
using TranslateWebApp.Interfaces;

namespace TranslateWebApp
{
    public class ExternalTranslationApi : IExternalTranslationApi
    {
        public const string apiKey = "googl-translate-token-api";

        public string GetServiceName()
        {
            return "Google Translate API";
        }

        public async Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en")
        {
            List<string> ret = new();

            TranslationClient client = TranslationClient.CreateFromApiKey(apiKey);
            IList<TranslationResult> results = await client.TranslateTextAsync(
                inputStrings,
                targetLanguage,
                sourceLanguage
            );

            foreach (TranslationResult result in results)
            {
                ret.Add(result.TranslatedText);
            }

            return ret;
        }
    }
}
