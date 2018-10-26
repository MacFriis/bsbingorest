using System;
using BSBingoApi.Infrastructure;

namespace BSBingoApi.Model
{
    public class BSWord : Resource
    {
        [Sortable]
        [Searchable]
        public string BsWord { get; set; }

        [Sortable(Default = true)]
        [Searchable]
        public string Category { get; set; }

        [Searchable]
        public string Language { get; set; }

        // public Form Create { get; set; }
    }
}
