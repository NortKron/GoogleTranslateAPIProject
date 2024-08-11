using Newtonsoft.Json;
using System.Text;

using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Clients
{
    public class RestTranslationClient : ITranslationService
    {
        private readonly HttpClient _httpClient;

        public RestTranslationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetServiceInfoAsync()
        {
            var response = await _httpClient.GetStringAsync("/api/translate");
            return response;
        }

        public async Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en")
        {
            string jsonData = JsonConvert.SerializeObject(
                new
                {
                    targetLanguage = targetLanguage,
                    sourceLanguage = sourceLanguage,
                    input = inputStrings
                });

            var requestContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/translate/translate-lines", requestContent);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(responseContent);
        }
    }
}
