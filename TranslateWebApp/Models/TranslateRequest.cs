namespace TranslateWebApp.Models
{
    public class TranslateRequest
    {
        public List<string> Input { get; set; }
        public string TargetLanguage { get; set; }
        public string SourceLanguage { get; set; }
    }
}
