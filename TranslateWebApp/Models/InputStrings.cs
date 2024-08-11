using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateWebApp.Models
{
    public class InputStrings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(256)]
        public string InputString { get; set; }

        [Required]
        public string TargetLanguage { get; set; }

        [Required]
        public string SourceLanguage { get; set; }

        [Required]
        public DateTimeOffset DateInput { get; set; }

        public InputStrings(string InputString, string targetLanguage, string sourceLanguage, DateTimeOffset DateInput)
        {
            this.InputString = InputString;
            this.TargetLanguage = targetLanguage;
            this.SourceLanguage = sourceLanguage;
            this.DateInput = DateInput;
        }
    }
}
