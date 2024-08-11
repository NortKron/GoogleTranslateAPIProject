using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Clients
{
    public class GrpcTranslationClient : ITranslationService
    {
        private readonly TranslationGRPCService.TranslationGRPCServiceClient _client;

        public GrpcTranslationClient(TranslationGRPCService.TranslationGRPCServiceClient client)
        {
            _client = client;
        }

        public async Task<string> GetServiceInfoAsync()
        {
            var request = new ServiceInfoRequest();
            var response = await _client.GetServiceInfoAsync(request);
            return response.Info;
        }

        public async Task<List<string>> TranslateAsync(List<string> inputStrings, string targetLanguage = "ru", string sourceLanguage = "en")
        {
            var request = new TranslateRequest
            {
                TargetLanguage = targetLanguage,
                SourceLanguage = sourceLanguage
            };

            request.Input.AddRange(inputStrings);

            var response = await _client.TranslateAsync(request);
            return response.Translations.ToList();
        }
    }
}
