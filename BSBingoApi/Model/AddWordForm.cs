using System;
using System.ComponentModel.DataAnnotations;

namespace BSBingoApi.Model
{
    public class AddWordForm
    {
        [Required]
        [Display(Name = "BSWord", Description = "The BS word to add to the collection")]
        public string BsWord { get; set; }

        [Required]
        [Display(Name = "language", Description = "The language the word is used (two letter isoxxxx code)")]
        public string Language { get; set; }
    }
}
