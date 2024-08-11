using TranslateWebApp.Models;
using Microsoft.EntityFrameworkCore;
using TranslateWebApp.Interfaces;

namespace TranslateWebApp.Data
{
    public class DataContext : DbContext, ITranslationCache
    {
        public DbSet<InputStrings> InputStrings { get; set; }
        public DbSet<Translations> Translations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InputStrings>().ToTable("InputStrings");
            modelBuilder.Entity<Translations>().ToTable("Translations");
        }

        public string GetCacheType() => "DataBase";
        public string GetCacheSize() => this.InputStrings.Count().ToString();

        public void InsertInputStrings(Models.TranslateRequest inputData)
        {
            foreach (string line in inputData.Input)
            {
                this.InputStrings.Add(
                    new InputStrings(line, inputData.TargetLanguage, inputData.SourceLanguage, DateTime.Now)
                );
            }

            this.SaveChanges();
        }

        public void AddTranslation(List<string> inputStrings, string sourceLanguage, string targetLanguage, List<string> translations)
        {
            
            foreach (string line in inputStrings)
            {
                this.InputStrings.Add(
                    new InputStrings(line, targetLanguage, sourceLanguage, DateTime.Now)
                );
            }

            foreach (string line in translations)
            {
                this.Translations.Add(new Translations(line));
            }

            this.SaveChanges();
        }
    }
}
