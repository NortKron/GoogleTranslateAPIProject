using Newtonsoft.Json;
using System.Net;
using System.Text;
using TranslateWebApp.Interfaces;

namespace REST_Client
{
    public class RestTranslationClient : ITranslationService
    {
        private readonly HttpClient _httpClient;

        public RestTranslationClient()
        {
            _httpClient = new HttpClient
            {
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact,
                BaseAddress = new Uri("https://localhost:61807")
            };
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
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(responseContent);

            return result["translations"];
        }
    }
}