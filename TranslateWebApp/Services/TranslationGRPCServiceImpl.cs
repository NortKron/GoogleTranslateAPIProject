using Grpc.Core;
using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Services
{
    public class TranslationGRPCServiceImpl : TranslationGRPCService.TranslationGRPCServiceBase
    {
        private readonly ITranslationService _translationService;

        public TranslationGRPCServiceImpl(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public override async Task<ServiceInfoResponse> GetServiceInfo(ServiceInfoRequest request, ServerCallContext context)
        {
            var info = await _translationService.GetServiceInfoAsync();

            return new ServiceInfoResponse 
            { 
                Info = info
            };
        }

        public override async Task<TranslateResponse> Translate(TranslateRequest request, ServerCallContext context)
        {
            var translations = await _translationService.TranslateAsync(request.Input.ToList(), request.TargetLanguage, request.SourceLanguage);
            
            var response = new TranslateResponse();
            response.Translations.AddRange(translations);
            return response;
        }
    }
}
