using System;
using Newtonsoft.Json;

namespace BSBingoApi.Model
{
    public class FormFieldOption
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        public object Value { get; set; }
    }
}
