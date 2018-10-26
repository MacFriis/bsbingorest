using System;
using BSBingoApi.Infrastructure;

namespace BSBingoApi.Model
{
    public class Language
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Name { get; set; }
    }
}
