using System;
using BSBingoApi.Infrastructure;

namespace BSBingoApi.Model
{
    public class Category : Resource
    {

        [Sortable(Default = true)]
        [Searchable]
        public string Name { get; set; }
    }
}
