using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranslateWebApp.Models
{
    public class Translations
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(256)]
        public string Translation { get; set; }

        public Translations(string Translation)
        {
            this.Translation = Translation;
        }
    }
}
