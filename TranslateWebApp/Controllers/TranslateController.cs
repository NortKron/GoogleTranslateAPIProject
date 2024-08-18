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

        /// <summary>
        /// Get info about service
        /// </summary>
        /// <returns>string</returns>
        [HttpGet]
        public async Task<IActionResult> about()
        {
            string serviceInfo = await _translationService.GetServiceInfoAsync();
            return Ok(serviceInfo);
        }

        /// <summary>
        /// Translate a list of strings
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /Todo
        ///     {
        ///         "TargetLanguage": "ru",
        ///         "SourceLanguage": "en",
        ///         "Input": [
        ///             "It is raining",
        ///             "I am happy",
        ///             "London is the capital of Great Britain"
        ///         ]
        ///     }
        /// </remarks>
        /// <param>JSON</param>
        /// <returns>List of translations</returns>
        [HttpPost("translate-lines")]
        //public async Task<JsonResult> translate(Object jsonObject)
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
