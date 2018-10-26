using System;
using Newtonsoft.Json;

namespace BSBingoApi.Model
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
