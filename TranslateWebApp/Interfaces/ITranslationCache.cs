namespace TranslateWebApp.Interfaces
{
    public interface ITranslationCache
    {
        public void AddTranslation(List<string> inputStrings, string sourceLanguage, string targetLanguage, List<string> translations);
        public string GetCacheType();
        public string GetCacheSize();
    }
}
