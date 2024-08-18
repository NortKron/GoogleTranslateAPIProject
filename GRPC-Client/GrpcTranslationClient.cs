using Grpc.Net.Client;
using GRPC_Client;
using TranslateWebApp.Interfaces;

public class GrpcTranslationClient : ITranslationService
{
    private readonly TranslationGRPCService.TranslationGRPCServiceClient _client;

    public GrpcTranslationClient()
    {
        _client = new TranslationGRPCService.TranslationGRPCServiceClient(
            GrpcChannel.ForAddress("https://localhost:61807")
        );
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

        var response = _client.Translate(request);
        return response.Translations.ToList();
    }
}
