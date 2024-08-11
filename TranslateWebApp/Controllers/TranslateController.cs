using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : Controller
    {
        private readonly ITranslationService _translationService;

        public TranslateController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet]
        public IActionResult about()
        {
            string serviceInfo = _translationService.GetServiceInfoAsync();
            return Ok(serviceInfo);
        }

        [HttpPost("translate-lines")]
        public async Task<JsonResult> translate(Object jsonObject)
        {
            TranslateRequest translateRequest = JsonConvert.DeserializeObject<TranslateRequest>(jsonObject.ToString());

            List<string> translationsList = await _translationService.TranslateAsync(
                translateRequest.Input.ToList(),
                translateRequest.TargetLanguage,
                translateRequest.SourceLanguage);

            return Json(
                new { 
                    translations = translationsList 
                });
        }
    }
}
