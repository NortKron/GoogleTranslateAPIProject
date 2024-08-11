using TranslateWebApp.Models;

namespace TranslateWebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any records
            if (context.InputStrings.Any() && context.Translations.Any())
            {
                return;   // DB has been seeded
            }

            var inputStrings = new InputStrings("test line", "ru", "en", DateTime.Now);
            context.InputStrings.Add(inputStrings);
            context.SaveChanges();
            int idLine = inputStrings.ID;

            var translations = new Translations("test translation");
            context.Translations.Add(translations);
            context.SaveChanges();
        }
    }
}
